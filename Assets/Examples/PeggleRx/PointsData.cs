using System;
using UnityEngine;

public struct PointsData : IEquatable<PointsData>
{
	public readonly int Points;
	public readonly GameObject Source;

	public PointsData(int points, GameObject source)
	{
		Points = points;
		Source = source;
	}

	public bool Equals(PointsData other)
	{
		return (Points, Source) != (other.Points, other.Source);
	}

	public override bool Equals(object obj)
	{
		return obj is PointsData other && Equals(other);
	}

	public override int GetHashCode() => (Points, Source).GetHashCode();
}