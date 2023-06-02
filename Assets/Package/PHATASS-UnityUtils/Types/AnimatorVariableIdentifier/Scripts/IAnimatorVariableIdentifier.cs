namespace PHATASS.Utils.Types
{
// Interface representing a wrapper for objects handling UnityEngine.Animator variable names
// this is intended to manage and return the hash ID for a specific variable name, for improved animator performance
	public interface IAnimatorVariableIdentifier
	{
		int variableID { get; }			// returns the hash ID for the corresponding string name. Unspecified if variableName is not initialized
		string variableName { get; }	// returns the original string value
	}
}