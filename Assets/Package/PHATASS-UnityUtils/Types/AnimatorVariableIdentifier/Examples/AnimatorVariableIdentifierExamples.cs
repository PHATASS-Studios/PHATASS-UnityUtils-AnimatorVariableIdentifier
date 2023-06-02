/*    PHATASS Studios 2023    */
//
//	EXAMPLE CODE - FEEL FREE TO REMOVE OR NOT IMPORT THIS FILE IF IT'S NOT NEEDED
//	these MonoBehaviours do not run as-is, and are only for illustrative purposes
//
//	AnimatorVariableIdentifier is a type handling the (de)serialization of the identifier for a UnityEngine.Animator variable name
//	this serializes the variable name as a string for editor purposes, then exposes the identifier as the more efficient hash
//
//	It can be simply assigned as a string through implicit casting, like so:
//
//		AnimatorVariableIdentifier animatorBoolID = "BoolID";
//
//	When creating or de-serializing this object, the ID hash is calculated and cached for faster, easier use.
//
//	It can then be used to access an animator variable.
//	Implicitly casts to int, returning cached variable ID, so the faster ID-using overload will be automatically used.
//
//		animator.SetBool(animatorBoolID, true);
//
//
//	None of its properties are publicly exposed other than the implicit cast to int.
//	To access its string value, it must be casted as its interface IAnimatorVariableIdentifier
//
//		Debug.Log((animatorBoolID as IAnimatorVariableIdentifier).variableName); //"BoolID"
//
//	In order to re-assign a variable name, simply re-assign it from a string.
//
//		animatorBoolID = "New-BoolID";
//

// Import the AnimatorVariableIdentifier type in the scripts you want to use it to handle animator variable names
using AnimatorVariableIdentifier = PHATASS.Utils.Types.AnimatorVariableIdentifier;

// ONLY IF you want to access the struct's members, you need to import the interface IAnimatorVariableIdentifier
using IAnimatorVariableIdentifier = PHATASS.Utils.Types.IAnimatorVariableIdentifier;

using UnityEngine;

//
//	The following examples show first how accessing a variable identifier is done traditionally with standard unity components.
//	Then, it shows how the AnimatorVariableIdentifier is used to make this process much faster and easier;
//
namespace Examples.AnimatorVariableIdentifierExamples
{
// SAMPLE A: Traditional way, no hash caching - MUCH SLOWER CODE
	public class AnimatorVariableAccess_TraditionalUnity_NoHashCaching : MonoBehaviour
	{
		[SerializeField]
		public string animatorVariableName = "VariableName";

		[SerializeField]
		public Animator animator;

		private void SetAnimatorBool (bool value)
		{
			// A string can be fed directly into the setter method, but this is slow if it needs to happen repeatedly during execution
			this.animator.SetBool(this.animatorVariableName, value);
		}
	}
//ENDOF SAMPLE A

// SAMPLE B: Traditional way using hash caching - FAST BUT BULKIER CODE
	public class AnimatorVariableAccess_TraditionalUnity_WithHashCaching : MonoBehaviour
	{
		[SerializeField]
		public string animatorVariableName = "VariableName";
		private int hashedVariableID;	//we need an additional variable to store our cached hash

		[SerializeField]
		public Animator animator;

		// We also need to initialize the hash value of our variable name
		//	bear in mind, if we were to change animatorVariableName after Start is executed, this cached hash would remain outdated causing bugs
		public void Start ()
		{
			this.hashedVariableID = UnityEngine.Animator.StringToHash(this.animatorVariableName); //https://docs.unity3d.com/ScriptReference/Animator.StringToHash.html
		}

		private void SetAnimatorBool (bool value)
		{
			//using the hashed ID this call is much faster, but we require a lot more code and are prone to causing bugs
			this.animator.SetBool(this.hashedVariableID, value);
		}
	}
//ENDOF SAMPLE B

// SAMPLE C: using AnimatorVariableIdentifier - FAST, LEAN, AND CLEAR CODE
	public class AnimatorVariableAccess_UsingAnimatorVariableIdentifier : MonoBehaviour
	{
		[SerializeField]
		public AnimatorVariableIdentifier animatorVariableID = "VariableName";	//We can easily initialize with a string - Serialized fields can be normally edited with a string box

		[SerializeField]
		public Animator animator;

		private void SetAnimatorBool (bool value)
		{
			// When feeding an AnimatorVariableIdentifier into an animator.SetVariable() method, it implicitly casts as int and returns a pre-cached hash value
			this.animator.SetBool(this.animatorVariableID, value);	//as fast as option B, as simple and error-free as option A
		}
	}
//ENDOF SAMPLE C
}