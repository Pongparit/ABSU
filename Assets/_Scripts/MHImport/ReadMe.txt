License: GNU Affero General Public License 3

Workflow:

Download the nightly build of MakeHuman. Place the the "MHImport.mhskel" file in the following directory: "[MakeHuman Directory]/data/rigs". You'll usually want to use this skeleton to export models into Unity3D.

Before exporting your model, make sure you have the pose set as "none". You will need to export your model as a .fbx file somewhere under the project assets directory for it to be used by MHImport.

To rig MakeHuman prefabs:

1. Select the prefab
2. Select "Rig"
3. Select "Humanoid" for "Animation Type" and click "Apply"
4. Press "Configure..."
5. Click the "Pose" dropdown in the inspector and select "Enforce T-Pose"
6. Click "Done" and apply your changes.

MHImport Tutorial:

1. Export your model as a .fbx file, placing it anywhere under the "Assets" directory.
2. After Unity has processed the models, open the MHImport window under Windows > MHImport.
3. Select the model's prefab and click "Process MakeHuman".

After the process is complete, which typically takes under 10 seconds, you will want to use the [model name]_proc prefab for your projects.
Do not delete the original model as the new prefab has dependencies linked to the .fbx file. This will not cause issues at build time.

MHImportSettings.cs contains the definitions for the hair and clothes. If you create or new hair/clothes are added to MakeHuman, you will need to add the name to the appropriate static array. Updates with the new definitions will be issued, but these often take several days to be approved and implemented in the Asset Store.