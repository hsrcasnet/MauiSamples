
using System.Drawing;

namespace iOSApp
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private UIButton myButton;
        private int counter = 0;

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
            this.Window.BackgroundColor = UIColor.White;

            // create a UIViewController with a single UIButton
            var vc = new UIViewController();

            this.myButton = new UIButton(new RectangleF(0, 0, 300, 30))
            {
                Center = this.Window.Center,
            };
            this.myButton.SetTitle("Click here!", UIControlState.Normal);
            this.myButton.SetTitleColor(UIColor.SystemBlue, UIControlState.Normal);
            this.myButton.TouchUpInside += this.MyButtonOnTouchUpInside;

            vc.View.AddSubview(this.myButton);

            this.Window.RootViewController = vc;

            // make the window visible
            this.Window.MakeKeyAndVisible();

            return true;
        }

        private void MyButtonOnTouchUpInside(object sender, EventArgs e)
        {
            this.counter++;
            this.myButton.SetTitle($"Clicked {this.counter}", UIControlState.Normal);
        }
    }
}
