# Tutorial: Using the Dialogue Graph Tool

## 1. Opening the Dialogue Graph Window
1. **Open the Unity Editor.**
2. Navigate to the menu bar and select `ChaoticDetectives` -> `DialogueGraph`.
3. The `DialogueGraph` window will open, displaying the interface as shown below.


## 2. Interface Overview
- **Toolbar:**
  - `File Name:` Text field to input the name of the dialogue file.
  - `Save Data`: Button to save the current dialogue graph.
  - `Load Data`: Button to load an existing dialogue graph.
  - `Create Node`: Button to create a new dialogue node.

- **Graph Area:**
  - This is where you will create and connect your dialogue nodes.
  - **MiniMap:** Located in the top-left corner for navigation.

## 3. Creating Nodes
1. Click the `Create Node` button in the toolbar.
2. A new dialogue node will appear in the graph area.

## 4. Connecting Nodes
1. Select the `Next` output port on the `Start` node.
2. Drag and connect it to the `Input` port on a dialogue node.
3. To add more choices to a dialogue node, click the `New Choice` button on the node.
4. New output ports labeled `Choice 1`, `Choice 2`, etc., will appear.
5. Drag these new output ports to connect to other dialogue nodes' `Input` ports.

## 5. Editing Node Text
1. Click on the dialogue node you want to edit.
2. Use the text field inside the node to change the dialogue text.
3. The node title will update to reflect the new text.

## 6. Saving the Dialogue Graph
1. Enter a file name in the `File Name:` text field.
2. Click the `Save Data` button.
3. The dialogue graph will be saved to a file in the `Assets/Resources` directory.

## 7. Loading an Existing Dialogue Graph
1. Enter the file name of the dialogue graph you want to load in the `File Name:` text field.
2. Click the `Load Data` button.
3. The graph will be loaded, and nodes will be populated based on the saved data.

## Example Workflow
1. **Create Dialogue Nodes:** Start by clicking the `Create Node` button to add your initial dialogue nodes.
2. **Connect Nodes:** Use the ports to create connections between nodes, defining the flow of dialogue.
3. **Add Choices:** For nodes requiring multiple responses, use the `New Choice` button to add additional output ports.
4. **Edit Dialogue:** Click on each node to update the dialogue text as needed.
5. **Save Progress:** Regularly save your progress by entering a file name and clicking the `Save Data` button.
6. **Load Data:** When reopening your project, use the `Load Data` button to continue from where you left off.

This tutorial should help you get started with using the Dialogue Graph tool in Unity. Feel free to explore and customize your dialogue system further! If you have any specific questions or run into issues, feel free to ask.
