using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
			var container = new StandardKernel();
			try
            {
                Application.EnableVisualStyles();
	            container.Bind<IUiAction>().To<SaveImageAction>();
	            container.Bind<IUiAction>().To<DragonFractalAction>();
	            container.Bind<IUiAction>().To<KochFractalAction>();
	            container.Bind<IUiAction>().To<ImageSettingsAction>();
	            container.Bind<IUiAction>().To<PaletteSettingsAction>();

	            container.Bind<Palette>().ToSelf().InSingletonScope();
				container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
	            container.Bind<IDragonPainterFactory>().ToFactory();

				Application.SetCompatibleTextRenderingDefault(false);
	            var mainForm = container.Get<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}