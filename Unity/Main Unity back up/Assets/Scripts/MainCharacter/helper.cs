using UnityEngine;
using System.Collections;

public static class helper
{
	//clamp angle method
	//allows to calculate four points for camera clipping/occlusion
/*	public struct ClipPlanePoints
	{
		public Vector3 UpLeft;
		public Vector3 UpRight;
		public Vector3 LowLeft;
		public Vector3 LowRight;
	}*/

	public static float ClampAngle(float angle, float min, float max)
	{
		do 
		{
			if(angle < -360)
				angle += 360;
			
			if(angle > 360)
				angle -= 360;
			
		}while(angle < -360 || angle > 360);
		
		return Mathf.Clamp (angle, min, max);
	}

	/*public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)
	{
		var clipPlanePoints = new ClipPlanePoints ();

		//is there a camera?
		if (Camera.main == null)
			return clipPlanePoints;

		//calculate height / width for points
		var transforms = Camera.main.transform;
		var halfFOV =  (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad; //convert to radians(degrees in 2d)
		var aspect = Camera.main.aspect;
		var distance = Camera.main.nearClipPlane;
		var height = distance * Mathf.Tan(halfFOV);
		var width =  height * aspect;//ratio of height based on aspect ratio

		clipPlanePoints.LowRight = pos + transforms.right * width;
		clipPlanePoints.LowRight -= transforms.up * height;
		clipPlanePoints.LowRight += transforms.forward * distance; // calculates one point

		clipPlanePoints.LowLeft = pos - transforms.right * width;
		clipPlanePoints.LowLeft -= transforms.up * height;
		clipPlanePoints.LowLeft += transforms.forward * distance; 

		clipPlanePoints.UpRight = pos + transforms.right * width;
		clipPlanePoints.UpRight += transforms.up * height;
		clipPlanePoints.UpRight += transforms.forward * distance; 

		clipPlanePoints.UpLeft = pos - transforms.right * width;
		clipPlanePoints.UpLeft += transforms.up * height;
		clipPlanePoints.UpLeft += transforms.forward * distance; 

		return clipPlanePoints;
	}*/
}
