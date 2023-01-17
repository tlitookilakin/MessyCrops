using HarmonyLib;
using System;
using System.Reflection;

namespace MessyCrops
{
	internal static class Utils
	{
		public static MethodInfo MethodNamed(this Type type, string name)
			=> AccessTools.Method(type, name);
		public static MethodInfo MethodNamed(this Type type, string name, Type[] args)
			=> AccessTools.Method(type, name, args);
		public static FieldInfo FieldNamed(this Type type, string name)
			=> AccessTools.Field(type, name);
	}
}
