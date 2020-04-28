inline fixed3 DepressLighting(fixed3 color, fixed3 baseColor, fixed weight = 1)
{
	return (color * 0.8 + 0.2) * baseColor * weight;
}