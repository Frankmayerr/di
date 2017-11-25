using System;
using System.Runtime.CompilerServices;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction   //, INeed<IImageHolder>
	{
		//private IImageHolder imageHolder;
		private readonly IDragonPainterFactory factory;
		private readonly Func<Random, DragonSettings> dragFunc;
		public DragonFractalAction(IDragonPainterFactory factory, Func<Random, DragonSettings> dragFunc)  //IDragonPainterFactory factory)
		{
			//this.imageHolder = imageHolder;
			this.factory = factory;
			this.dragFunc = dragFunc;
		}

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			/*new DragonPainter(imageHolder, dragonSettings).Paint()*/;
			// создаём painter с такими настройками
			factory.Create(dragonSettings).Paint();

		}

		private DragonSettings CreateRandomSettings()
		{
			return dragFunc(new Random());
			//return new DragonSettingsGenerator(new Random()).Generate(); // Func<Random, Generator>
		}
	}
}