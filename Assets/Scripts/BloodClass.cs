using System;

public class BloodClass
{
	public readonly BloodType BloodType;
	public readonly bool Rh;

	public BloodClass()
	{
		var random = new Random();
		BloodType = (BloodType)Enum.GetValues(typeof(BloodType)).GetValue(random.Next(4));
		Rh = random.Next(2) == 0;
	}
}
