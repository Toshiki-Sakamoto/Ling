//
// RoadInstaller.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ling.Map.Builder.Road
{
	public class RoadInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindFactory<SimpleRoad, SimpleRoad.Factory>();

			Container
				.BindFactory<BuilderConst.RoadType, IRoadBuilder, RoadBuilderFactory>()
				.FromFactory<CustomRoadFactory>();
		}
	}
}
