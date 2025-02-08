# 💬 FM-Dialogues
Flexible node based branching dialogue system

![dialoguegraph](https://github.com/user-attachments/assets/8ecb59bf-9c96-479c-a38a-afe2700d465e)

## 📦 Dependencies
- FM-Core: https://github.com/Felis-Major/FM-Core.git

## 🛰 Concepts
This dialogue system has 2 core ideas: **Editor Nodes** and **Runtime Nodes**.  
- **Editor Nodes** are the interface through which you'll be able to edit node data. You can use the default ones provided in this package or create your owns.  
- **Runtime Nodes** hold the data of each node on the graph. They can be read using a **Dialogue Reader**.

## 🚧 Getting Started
### ▶ Start using the dialogue nodes:
- Create a new Dialogue File (right click in your project folder -> *FM/Dialogues/Dialogue File*).  
- Open up the dialogue editor window. To do so go to FM -> Dialogues -> Dialogue Editor.  
- Open your previously created dialogue file by clicking on the dot in the dialogue file field.

You should see a new empty graph, with only a green **Start Node**.  
The **Start Node** represents the start of the graph. The first node that will be connected to the **Start Node** will be flagged as the first node of the graph.  
### ▶ Create a new node
- Right click anywhere on the graph, and select "Create Node"
- In the window that opens, select the type of node you wish to create
- Connect the **Next** port of the **Start Node** to the **Input** port of the node you just created

Repeat this process until your dialogue is complete.

### ▶ Reading your graph
You will now need to read the graph you just created.
- Attach a ```DialogueReader``` component to an empty GameObject in your scene
- To start the dialogue sequence, call the StartDialogue method on the ```DialogueReader``` or simply drag & drop your dialogue file in the default dialogue file field, and check Play On Awake.

If you press play, the first node of your graph will be processed. Now you'll need to link some sort of input to trigger the next node.  
To do so call the ```GoToNextNode()```. If you have a node that requires more informations or user action before going to the next node (for example, the ```DialogueBranchNode```) simply call that method on your node before calling ```GoToNextNode()```.  
At this point if you press play, you should be able to go through your entire dialogue.

## ⚙ Extending the editor
This dialogue system allows for great flexibility if you need to add custom parameters to some nodes, or even create custom nodes.

### ▶ Creating custom nodes
To create a new custom node, you'll need two classes. A **Runtime Node** and its **Editor Node** version.  
#### Creating the **Runtime Node**
- Create a new class and inherit from ```RuntimeNode```
- Create Serialized Fields that will hold your data (see ```DialogueLineRuntimeNode``` for an example)
- Override the ```GetNextNode()``` method. This method will enable you to tell the graph where to go when this node is processed. To find a node using the name of a port, use the ```GetConnectedNodesToPort()``` method. Make sure that you only store runtime informations in this class, otherwise your project won't build.  

#### Creating the **Editor Node**
- Create a new class inside an **Editor** folder, and inherit from ```GenericBaseNode<T>``` where T is the type of your RuntimeNode.
- To configure the node, you can use various attributes:
  - **Mandatory** | ```RuntimeNodeType```: Tells the editor which RuntimeType is associated with this node.
  - **Optional** | ```NodeName```: Specify the name of the node on the graph.
  - **Optional** | ```NodeColor```: Set the color of the node in the graph.
  - **Optional** | ```NodeWidth```: Sets the width of the node, in graph units (1 = 25px).
  - **Optional** | ```SingleInstanceNode```: Only allows one node of this type per graph.
- To define fields and ports on your node, call the following methods in the constructor of the node:
  - ```_nodePortHandler.AddInputPort()``` or ```_nodePortHandler.AddOutputPort``` to add a port.
  - ```_nodeFieldHandler.AddField``` to add a field.
- Override both Save & Load methods, they will allow your node to store its data into the RuntimeNode. 
  - To get the value of a field, use the ```_nodeFieldHandler.GetFieldValue()``` method and pass in the name of the field.
  - To set the value of a field, use the ```_nodeFieldHandler.SetFieldValue()``` method and pass in the name of the field.   

### ▶ Custom dialogue file
In order for the graph to know which types of nodes to use, you need to create a new DialogueFile.
- Create a new class and inherit from ```DialogueFile```
- For each RuntimeNode that you created or want to see in the graph, add a new ```SupportedNodeType``` attribute to the class and give it the RuntimeNode type you want to support. 
At this point you should be able to create a new dialogue file of the type you just created, load it into the graph and use your custom nodes.

### ▶ Custom Dialogue Reader
Finally, to read your new dialogue file you'll need to handle new node types.
- Create a new ```MonoBehaviour``` and inherit from ```BaseDialogueReader```
- Override the method called ```ProcessNode```. This will be called whenever a new node is ready to be processed. In this method, you can define for each type of nodes how you want them to be processed. You can check against the type of each ```RuntimeNode``` you are using to handle node processing.
