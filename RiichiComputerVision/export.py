from ultralytics import YOLO

if __name__ == '__main__':

    # Load a trained YOLO11n model
    model = YOLO("runs/detect/train/weights/best.pt")

    # Export the model to ONNX format
    model.export(format="onnx", dynamic=True, simplify=True, device=0, data="data/data.yaml", nms=True, conf=0.25, iou=0.45)