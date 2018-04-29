package md5d5d7085dcc4bc2c637d940cbc9d2a78a;


public class EspecialidadesActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("App.Activitys.EspecialidadesActivity, App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EspecialidadesActivity.class, __md_methods);
	}


	public EspecialidadesActivity ()
	{
		super ();
		if (getClass () == EspecialidadesActivity.class)
			mono.android.TypeManager.Activate ("App.Activitys.EspecialidadesActivity, App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
