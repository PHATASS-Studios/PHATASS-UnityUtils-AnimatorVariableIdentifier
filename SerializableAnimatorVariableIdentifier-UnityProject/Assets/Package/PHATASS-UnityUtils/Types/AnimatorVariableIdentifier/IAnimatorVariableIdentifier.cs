namespace PHATASS.Utils.Types
{
	//Interface representing a wrapper for objects handling UnityEngine.Animator variable names
	//this is intended to manage and return the hash ID for a specific variable name, for improved animator performance
	public interface IAnimatorVariableIdentifier
	{
		int variableID { get; }
		string variableName { get; }
	}
}