using UnityEngine;

namespace PHATASS.Utils.Types
{
//
//	Struct handling the (de)serialization of the identifier for a UnityEngine.Animator variable name
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
//
	[System.Serializable]
	public struct AnimatorVariableIdentifier :
		IAnimatorVariableIdentifier,
		ISerializationCallbackReceiver
	{
	//Serialized fields
		[SerializeField]
		private string variableName;
	//ENDOF Serialized fields

	//IAnimatorVariableIdentifier
		int IAnimatorVariableIdentifier.variableID { get { return this.variableID; }}
		string IAnimatorVariableIdentifier.variableName { get { return this.variableName; }}
	//ENDOF IAnimatorVariableIdentifier

	//ISerializationCallbackReceiver
		//OnBeforeSerialize empty - no help needed with serialization
		void ISerializationCallbackReceiver.OnBeforeSerialize () {}
		//cache variable name's hash on deserialization
		void ISerializationCallbackReceiver.OnAfterDeserialize () { this.Init(this.variableName); }
	//ENDOF ISerializationCallbackReceiver

	//Constructor 
		public AnimatorVariableIdentifier (string name)
		{	
			this.variableName = name;
			this.variableID = 0;
			this.Init(name);
		}
		private void Init (string name)
		{ this.variableID = Animator.StringToHash(name); }
	//ENDOF Constructor

	//private fields
		private int variableID;
	//ENDOF fields

	//operator overrides
		//conversion to int used to expose the cached hash value
		public static implicit operator int (AnimatorVariableIdentifier identifierObject)
		{ return identifierObject.variableID; }

		//implicit conversion from string allows easy intialization
		public static implicit operator AnimatorVariableIdentifier (string name)
		{ return new AnimatorVariableIdentifier(name: name); }
	//ENDOF operator overrides
	}
}

/*    PHATASS Studios 2023    */
