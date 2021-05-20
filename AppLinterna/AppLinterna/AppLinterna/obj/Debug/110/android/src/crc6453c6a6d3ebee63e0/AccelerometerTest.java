package crc6453c6a6d3ebee63e0;


public class AccelerometerTest
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AppLinterna.AccelerometerTest, AppLinterna", AccelerometerTest.class, __md_methods);
	}


	public AccelerometerTest ()
	{
		super ();
		if (getClass () == AccelerometerTest.class)
			mono.android.TypeManager.Activate ("AppLinterna.AccelerometerTest, AppLinterna", "", this, new java.lang.Object[] {  });
	}


	public AccelerometerTest (int p0)
	{
		super (p0);
		if (getClass () == AccelerometerTest.class)
			mono.android.TypeManager.Activate ("AppLinterna.AccelerometerTest, AppLinterna", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
