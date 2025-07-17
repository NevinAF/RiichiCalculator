using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Perception.GroundTruth.LabelManagement;

public static class Helper
{
	[MenuItem("CONTEXT/IdLabelConfig/Add Tiles")]
	public static void AddTiles(MenuCommand command)
	{
		IdLabelConfig config = command.context as IdLabelConfig;
		if (config == null) return;


		// Mahjong tiles:
		// 1B, 2B, 3B, 4B, 5B, 6B, 7B, 8B, 9B,
		// 1C, 2C, 3C, 4C, 5C, 6C, 7C, 8C, 9C,
		// 1D, 2D, 3D, 4D, 5D, 6D, 7D, 8D, 9D,
		// GD, RD, WD, EW, SW, WW, NW, XX (back)
		config.Init(new IdLabelEntry[] {
			new IdLabelEntry { label = "1 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "2 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "3 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "4 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "5 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Red 5 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "6 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "7 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "8 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "9 Bamboo", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "1 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "2 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "3 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "4 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "5 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Red 5 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "6 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "7 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "8 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "9 Character", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "1 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "2 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "3 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "4 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "5 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Red 5 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "6 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "7 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "8 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "9 Dot", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Green Dragon", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Red Dragon", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "White Dragon", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "East Wind", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "South Wind", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "West Wind", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "North Wind", hierarchyRelation = HierarchyRelation.Independent },
			new IdLabelEntry { label = "Tile Back", hierarchyRelation = HierarchyRelation.Independent }
		});
		EditorUtility.SetDirty(config);
	}
}
