using System;
using System.Collections.Generic;
using System.Linq;

public class BloodClass
{
	public readonly BloodType BloodType;
	public readonly bool Rh;

	public BloodClass(IEnumerable<BloodType> allowedBloodTypes, IEnumerable<bool> allowedRh)
	{
		var random = new Random();
		
		var bloodTypes = allowedBloodTypes.ToList();
		BloodType = bloodTypes.ElementAt(random.Next(bloodTypes.Count));
		
		var rh = allowedRh.ToList();
		Rh = rh.ElementAt(random.Next(rh.Count));
	}
	
	public BloodClass(BloodType bloodType, bool rh)
	{
		BloodType = bloodType;
		Rh = rh;
	}

	public override bool Equals(object obj)
	{
		if (obj is not BloodClass other) return false;

		return BloodType == other.BloodType && Rh == other.Rh;
	}
	
	public override int GetHashCode()
	{
		return HashCode.Combine((int)BloodType, Rh);
	}
}
