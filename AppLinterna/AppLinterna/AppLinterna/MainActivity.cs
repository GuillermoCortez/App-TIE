using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace AppLinterna
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        bool state = false;
        List<double> xValues = new List<double>();
        bool one = false;
        bool two = false;
        bool three = false;
        int _one = 0;
        int _two = 0;
        double sens = 2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.sens);
            seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                

                sens = 4 - ( 1 + Convert.ToDouble(e.Progress) * 2 / 99);
                //textSens.Text = sens.ToString();
            };

            TextView textSens = FindViewById<TextView>(Resource.Id.textSens);
            textSens.SetTextColor(Android.Graphics.Color.White);

            //ACEL---------------------
            SensorSpeed speed = SensorSpeed.UI;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            ToggleAccelerometer(speed);


            //LINTERNA--------------------------------------------------
            ImageButton btn = FindViewById<ImageButton>(Resource.Id.btnOn);
            
            btn.Click += async (sender, e) =>
            {
                if (state)
                {
                    await Flashlight.TurnOffAsync();
                    state = false;
                    btn.SetImageResource(Resource.Drawable.btn_off);
                }
                else
                {
                    await Flashlight.TurnOnAsync();
                    state = true;
                    btn.SetImageResource(Resource.Drawable.btn_on);
                }
                
                
            };

            
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            
            double x = data.Acceleration.X;
            //sensorX.Text = "Sensor X:" + x;
            //double sens = 1.5;
            one = one || x > sens ? true : false;

            if (one && _one < 7)    //Se dio el primer gesto por lo que se analizan las  siguientes 7 lecturas
            {
                if (x < sens)       //Se dio el segundo gesto por lo cual reiniciamos las variables del primer gesto.
                {
                    two = true;
                    _one = -1;
                    one = false;
                }
                _one++;             //Se suma el n lecturas 
                
            }
            else
            {
                one = false;
                _one = 0;
            }


            if (two && _two < 7)    //Se dio el segundo gesto por lo que se analizan las  siguientes 7 lecturas
            {
                if (x > sens)       //Se dio el segundo gesto por lo cual reiniciamos las variables del primer gesto.
                {
                    three = true;
                    _two = -1;
                    two = false;
                }
                _two++;             //Se suma el n lecturas 

            }
            else
            {
                two = false;
                _two = 0;
            }





            if (three)
            {
                ImageButton btn = FindViewById<ImageButton>(Resource.Id.btnOn);
                if (state)
                {
                    Flashlight.TurnOffAsync();
                    state = false;
                    btn.SetImageResource(Resource.Drawable.btn_off);
                }
                else
                {
                    Flashlight.TurnOnAsync();
                    state = true;
                    btn.SetImageResource(Resource.Drawable.btn_on);
                }
                
                one = false;
                two = false;
                three = false;

            }
            
            xValues.Add(Math.Round(data.Acceleration.X, 3));

        }

        public void ToggleAccelerometer(SensorSpeed speed)
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
