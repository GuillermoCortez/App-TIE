using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

public class GyroscopeTest : AppCompatActivity
{
    // Set speed delay for monitoring changes.
    SensorSpeed speed = SensorSpeed.UI;
    TextView sensorX = FindViewById<TextView>(Resource.Id.textViewX);
    public GyroscopeTest()
    {
        // Register for reading changes.
        Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
        

        
    }

    void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
    {
        var data = e.Reading;
        // Process Angular Velocity X, Y, and Z reported in rad/s
        sensorX.Text = "Sensor X:"  + data.AngularVelocity.X;
    }

    public void ToggleGyroscope()
    {
        try
        {
            if (Gyroscope.IsMonitoring)
                Gyroscope.Stop();
            else
                Gyroscope.Start(speed);
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
}