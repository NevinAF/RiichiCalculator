from ultralytics.data.converter import convert_coco

# For keypoints data (like person_keypoints_val2017.json)
convert_coco(
    labels_dir="coco",  # Directory containing your json file
    save_dir="output",
    cls91to80=False
)