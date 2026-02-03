I used **ChatGPT** as an AI helper.

I sent 38 messages to ChatGPT.

It took nearly 10 hours.

The most frequently requested help concerns ScriptableObjects and how to create a system that is extensible.



Points where I received help:



1\. Interface Usage

&nbsp;	I used interfaces to handle the interactions between objects and characters. This prevented any object from referencing another, thus avoiding unnecessary RAM accumulation. I executed both functions and passed variables in this way.



2\. Raycast and Interaction System

 	I discussed how I could use the interface of the object I was viewing with Ray.



3\. Input System

 	I designed the input system in three different ways: it can work directly, a desired InputAction can be assigned, and a desired key can be assigned.



4\. ScriptableObjects

&nbsp;	I discussed the basic principles and why and how it should be used. I also discussed how to create a system that is easily expandable.



Some questions that I asked to ChatGPT:

* How can I send a message to the interface of the object I'm interacting with?
* How can I use an interface to transfer data from a character to the object I'm interacting with?
* Input systems can be implemented in several different ways. Which one is more optimized and user-friendly?
* What are the advantages of ScriptableObjects in this type of system? Wouldn't creating different data for each object increase the workload and complexity?



Note:

I mostly used Unreal Engine, and I designed the system to work the same way as in that project. Although it's a small project, I didn't want objects to reference each other. That's why I used an interface system. A clear design approach allows this system to be built more firmly from the ground up.

So, from the very beginning, I exchanged ideas with ChatGPT about how such a basic system could be built using an interface.

I had never worked professionally with Unity before.

