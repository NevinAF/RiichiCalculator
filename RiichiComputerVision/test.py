from ultralytics import YOLO

if __name__ == '__main__':

    # Load a trained YOLO11n model
    model = YOLO("runs/detect/train/weights/best.pt")

    # Test all of the images in the test set
    results = model(source='data/images/real', save=True, project='results', name='REAL')