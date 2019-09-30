//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mime;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ConsoleApp2
////{
//static class LightsController
//{
//    static LightsForm form;

//    // Выделяйте методы с говорящими названиями.
//    // Названия аргументов метода должны снимать неоднозначности. 
//    Тут уточнили, что время в секундах.
//        // Для переименования, нажмите правой кнопкой на имя, затем Refactor->Rename
//        static void Wait(double timeInSeconds)
//        {
//            Thread.Sleep((int)(timeInSeconds * 1000));
//        }

//        // Основной принцип программирование: Don't Repeat Yourself. 
//        // Не повторять один и тот же код дважды. 
//        // Если при создании программы вы пользовались копированием и вставкой
//        // из буфера обмена, то в коде необходимо прибраться, удалив повторяющиеся части.
//        // Кроме того, старайтесь, чтобы каждый метод был размером не больше экрана.
//        static void Control()
//        {
//            Wait(1);
//            while (true)
//            {
//                form.LightOn(0);
//                Wait(1);
//                form.LightOn(1);
//                Wait(1);
//                form.LightOff(0);
//                form.LightOff(1);
//                form.LightOn(2);
//                Wait(1);
//                //Здесь мы заменили повторяющиеся строчки циклом
//                for (int i = 0; i < 5; i++)
//                {
//                    form.LightOff(2);
//                    Wait(0.25);
//                    form.LightOn(2);
//                    Wait(0.25);
//                }
//                form.LightOff(2);
//                form.LightOn(1);
//                Wait(1);
//                form.LightOff(1);
//            }
//        }

//        public class LightsForm : Form
//        {
//            bool[] lights = new bool[3];

//            public LightsForm()
//            {
//                DoubleBuffered = true;
//            }

//            protected override void OnPaint(PaintEventArgs e)
//            {
//                var d = Math.Min(ClientSize.Width, ClientSize.Height / 3);
//                var colors = new[] { Color.Red, Color.Yellow, Color.Green };
//                e.Graphics.Clear(Color.White);
//                for (int i = 0; i < 3; i++)
//                    e.Graphics.FillEllipse(
//                        new SolidBrush(lights[i] ? colors[i] : Color.White),
//                        ClientSize.Width / 2 - d / 2,
//                        i * ClientSize.Height / 3 + ClientSize.Height / 6 - d / 2,
//                        d,
//                        d);
//            }

//            public void LightOn(int lightColor)
//            {
//                lights[lightColor] = true;
//                BeginInvoke(new Action(Invalidate));
//            }

//            public void LightOff(int lightColor)
//            {
//                lights[lightColor] = false;
//                BeginInvoke(new Action(Invalidate));
//            }

//            [STAThread]
//            [Test]
//            [Explicit]
//            public static void Main()
//            {
//                MediaTypeNames.Application.EnableVisualStyles();
//                form = new LightsForm();
//                new Action(Control).BeginInvoke(null, null);
//                MediaTypeNames.Application.Run(form);
//            }
//        }
//    }
//}
