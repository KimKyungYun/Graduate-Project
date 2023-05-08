
### IMPORTANT : Move the "Resources" folder to the root of your project.

### To access the prefabs for Full machine and Assembled parts, please unpack "Prefabs > Nested Prefabs.unitypackage" 
### This was a needed workaround to be able to submit to the Asset Store because the 
### Validator recognize nested LOD prefabs as invalid for seemingly no reasons. -_-#

### Unpack the HDRP, URP or Builtin .unitypackages depending on your target pipeline.

### Scripts

DeltaXYZ Script :

		float armsLenght : Lenght of the arms from the head Object axis to the carriages axis
		
		GameObject headObject : Offset object for the head
		
		GameObject headAnchor : Anchor object for the head
		
		GameObject bedAnchor : Anchor object for the bed
		
		GameObject XHeadAxis : Anchor object for the X Axis on the head
		
		GameObject XCarriageAxis : Anchor object for the X Axis on the carriage
		
		GameObject XCarriage : Carriage object on the X tower
		
		GameObject YHeadAxis : Anchor object for the Y Axis on the head
		
		GameObject YCarriageAxis : Anchor object for the Y Axis on the carriage
		
		GameObject YCarriage : Carriage object on the Y tower
		
		GameObject ZHeadAxis : Anchor object for the Z Axis on the head
		
		GameObject ZCarriageAxis : Anchor object for the Z Axis on the carriage
		
		GameObject ZCarriage : Carriage object on the Z tower

		Vector2 bedSize : Size of the bed

		float height : Maximum height of the area

		float speed : Head speed from target to target

		bool bedNotMoving : To use when the bed is not a moving object (see Laser cutter / CNC prefab)

		Vector3[] targets : List of targets

		int posIndex : Index of the current target in the list

		bool dontUseList : Disable the use of target list

		bool randomHorizontalTarget : Generate target at random horizontal values

		bool randomVerticalTarget : Generate target at random vertical values

		List<ScrollMaterialMapper> scrollMats : List of ScrollMaterialMapper* attached to the machine

		float averageNoisePitch : Pitch of the machine eletronic buzz sound

		float volume : Volume of the machine eletronic buzz sound
		
		
		
CartesianXYZ Script :

		GameObject XAxisAnchor : Anchor object for the X Axis

		GameObject YAxisAnchor : Anchor object for the Y Axis
				
		GameObject ZAxisAnchor : Anchor object for the Z Axis
				
		GameObject BedAnchor : Anchor object for the Bed

		Vector2 bedSize : Size of the bed

		float height : Maximum height of the area

		float speed : Head speed from target to target

		bool bedNotMoving : To use when the bed is not a moving object (see Laser cutter / CNC prefab)

		Vector3[] targets : List of targets

		int posIndex : Index of the current target in the list

		bool dontUseList : Disable the use of target list

		bool randomHorizontalTarget : Generate target at random horizontal values

		bool randomVerticalTarget : Generate target at random vertical values

		List<ScrollMaterialMapper> scrollMats : List of ScrollMaterialMapper* attached to the machine

		float averageNoisePitch : Pitch of the machine eletronic buzz sound

		float volume : Volume of the machine eletronic buzz sound
		
		
ScrollMaterialMapper

		Renderer Renderer : Renderer of the object whom material will be sync
		
		bool invertDirection : Toggle if the initial scroll movement should go in the opposite direction
		
		Axis triggersOnAxis : Indicate which axis movement trigger material scrolling
		
LaserDrawer

		List<GameObject> points : List of dummy gameobjects as path for the laser beam
	
		float raycastDistance : Laser distance on final gameobject
		
CableDrawer

		endGo : Dummy gameobject as the end of the cable spline
		
		normalIntensity : Tengeant intensity for the cable curve
		
		alwaysUpdate : To use for debugging/setting up, refresh mesh at every update instead of movement
		
ExtrusionAssetResources

		Singleton for easy resources access
		
ExtrusionBuilder

	Size : Switch between extrusion sizes
	
	Add part : add a part to the List

	Flip Front/Back
	Add 90° rotation
	Add 180° Rotation