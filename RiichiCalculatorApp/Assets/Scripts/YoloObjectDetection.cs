using UnityEngine;
using Unity.InferenceEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace ComputerVision
{
    [CreateAssetMenu(fileName = "YoloObjectDetection", menuName = "Inference Engine/Object Detection/Yolo")]
    public class YoloObjectDetection : ScriptableObject
    {
        /// <summary>
        /// Represents a single prediction from the Yolo nms model, formatting the 6 tensor values into a more usable structure.
        /// </summary>
        public struct Prediction
        {
            /// <summary>
            /// Bounding box in normalized coordinates (0 to 1).
            /// </summary>
            public Rect BoundingBox;
            /// <summary>
            /// Confidence score of the prediction, ranging from 0 to 1.
            /// </summary>
            public float Confidence;
            /// <summary>
            /// Class ID of the predicted object.
            /// </summary>
            public int ClassId;
        }

        [Tooltip("The model asset to use for object detection. This is expected to be a Yolo model exported with nms=true, resulting in a single input tensor [batches, 3, inputWidth, inputHeight] and a single output tensor [batches, maxOutputCount, 6].")]
        [SerializeField] private ModelAsset modelAsset;
        [Tooltip("Number of batches to process in parallel. This should match the model's batches [batches, 3, inputWidth, inputHeight].")]
        [SerializeField] private int batches = 1;
        [Tooltip("Width of the input tensor. This should match the model's input width [batches, 3, inputWidth, inputHeight].")]
        [SerializeField] private int inputWidth = 640;
        [Tooltip("Height of the input tensor. This should match the model's input height [batches, 3, inputWidth, inputHeight].")]
        [SerializeField] private int inputHeight = 640;
        [Tooltip("Maximum number of output predictions to read back. This should match the model's max output count [batches, maxOutputCount, 6].")]
        [SerializeField] private int maxOutputCount = 300;

        /// <summary>
        /// Input tensor for the model, initialized to be of shape [batches, 3, inputWidth, inputHeight].
        /// </summary>
        private Tensor<float> m_inputTensor;
        
        /// <summary>
        /// Worker instance that handles the model inference, generated directly from the model asset.
        /// </summary>
        private Worker m_Worker;

        /// <summary>
        /// CommandBuffer used to schedule the texture conversion and model inference.
        /// </summary>
        private CommandBuffer m_commandBuffer;

        public int Batches => batches;
        public int InputWidth => inputWidth;
        public int InputHeight => inputHeight;
        public int MaxOutputCount => maxOutputCount;
        public Tensor<float> InputTensor => m_inputTensor;
        public Tensor<float> OutputTensor => m_Worker?.PeekOutput() as Tensor<float>;

        private void Awake()
        {
            m_inputTensor = new Tensor<float>(new TensorShape(batches, 3, inputHeight, inputWidth));

            m_Worker = new Worker(ModelLoader.Load(modelAsset), BackendType.GPUCompute);
            m_Worker.SetInput(0, m_inputTensor);

            m_commandBuffer = new CommandBuffer { name = "Yolo Detect - " + modelAsset.name };
        }

        public void SetTexture(Texture texture, bool dynamic)
        {
            m_commandBuffer.Clear();

            m_commandBuffer.ToTensor(texture, m_inputTensor);

            // If texture is static (does not change), we execute the conversion immediately. Also, clear the buffer because this does not need to happen again.
            if (!dynamic)
            {
                Graphics.ExecuteCommandBuffer(m_commandBuffer);
                m_commandBuffer.Clear();
            }

            m_commandBuffer.ScheduleWorker(m_Worker);
        }

        public void Execute()
        {
            if (m_commandBuffer == null)
            {
                Debug.LogError("CommandBuffer is not initialized.");
                return;
            }

            Graphics.ExecuteCommandBuffer(m_commandBuffer);
        }

        public IEnumerable<Prediction> ReadbackPredictions(int batch = 0)
        {
            var outputTensor = OutputTensor;
            if (outputTensor == null)
                yield break;

            var readback = outputTensor.ReadbackAndClone();

            // Only process the first batch right now.
            int max = Mathf.Min(maxOutputCount, readback.shape[1]);
            for (int index = 0; index < max; index++)
            {
                float score = readback[batch, index, 4];
                if (score < float.Epsilon) // Threshold is baked into the model.
                    yield break;
                    
                float x1 = readback[batch, index, 0];
                float y1 = readback[batch, index, 1];
                float x2 = readback[batch, index, 2];
                float y2 = readback[batch, index, 3];

                yield return new Prediction
                {
                    BoundingBox = new Rect(
                        x1,
                        y1,
                        x2 - x1,
                        y2 - y1),
                    Confidence = score,
                    ClassId = (int)readback[batch, index, 5]
                };
            }

            readback.Dispose();
        }

        private void OnDestroy()
        {
            m_commandBuffer.Release();
            m_Worker.Dispose();
            m_inputTensor.Dispose();
        }
    }
}