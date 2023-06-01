using UnityEngine;

namespace PHATASS.Utils.Types
{
	//Class handling the (de)serialization of the identifier for a UnityEngine.Animator variable name
	//this serializes the variable name as a string for editor purposes, then exposes the identifier as the more efficient hash
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

		public static implicit operator AnimatorVariableIdentifier (string name)
		{ return new AnimatorVariableIdentifier(name: name); }
	//ENDOF operator overrides
	}
}