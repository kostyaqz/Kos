//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp2
//{
//    enum Lights
//    {
//        Red = 0,
//        Yellow = 1,
//        Green = 2
//    }

//    static class LightsController
//    {
//        static LightsForm form;

//        // Выделяйте методы с говорящими названиями.
//        // Названия аргументов метода должны снимать неоднозначности. 
//        // Тут уточнили, что время в секундах.
//        // Для переименования, нажмите правой кнопкой на имя, затем Refactor->Rename
//        static void Wait(double timeInSeconds)
//        {
//            Thread.Sleep((int)(timeInSeconds * 1000));
//        }

//        static void LightOn(Lights color)
//        {
//            form.LightOn((int)color);
//        }

//        static void LightOff(Lights color)
//        {
//            form.LightOff((int)color);
//        }

//        // Вместо непонятных чисел, используйте именованные константы с осмысленными именами. 
//        // Так код будет понятнее
//        const int BlinkingCount = 5;
//        const double LightDuration = 1;
//        const double BlinkingTime = 0.25;

//        // Основной принцип программирования: Don't Repeat Yourself. 
//        // Не повторять один и тот же код дважды. 
//        // Если при создании программы вы нажимали Ctrl-C + Ctrl-V - в ней что-то не так.
//        static void SwitchTo(Lights color)
//        {
//            LightOff(Lights.Red);
//            LightOff(Lights.Yellow);
//            LightOff(Lights.Green);
//            LightOn(color);
//        }

//        private static void Blink()
//        {
//            // используйте циклы для повторяющихся действий
//            for (int i = 0; i < BlinkingCount; i++)
//            {
//                LightOff(Lights.Green);
//                Wait(BlinkingTime);
//                LightOn(Lights.Green);
//                Wait(BlinkingTime);
//            }
//        }

//        // Старайтесь, чтобы каждый метод был размером не больше экрана.
//        static void Control()
//        {
//            Wait(LightDuration);
//            while (true)
//            {
//                // Теперь в подсказке после открытия скобки видно, 
//                // что нужно передать цвет, причем только один из трех:
//                SwitchTo(Lights.Red);
//                Wait(LightDuration);
//                LightOn(Lights.Yellow);
//                Wait(LightDuration);
//                SwitchTo(Lights.Green);
//                Wait(LightDuration);
//                Blink();
//                SwitchTo(Lights.Yellow);
//                Wait(LightDuration);
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
//                Application.EnableVisualStyles();
//                form = new LightsForm();
//                new Action(Control).BeginInvoke(null, null);
//                Application.Run(form);
//            }
//        }
//    }
//}
