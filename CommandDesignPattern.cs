/* Command decouples the object that invokes the operation from the one that knows how to perform it.
The base class contains an execute() method that simply calls the action on the receiver.
Command objects can be thought of as "tokens" that are created by one client that knows what need to be done, 
and passed to another client that has the resources for doing it.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // Command Interface
    interface ICommand
    {
        void Execute();
    }

    // Concrete Commands
    class LightOnCommand : ICommand
    {
        private readonly Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.TurnOn();
        }
    }

    class LightOffCommand : ICommand
    {
        private readonly Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.TurnOff();
        }
    }

    // Receiver
    class Light
    {
        public void TurnOn()
        {
            Console.WriteLine("Light is on");
        }

        public void TurnOff()
        {
            Console.WriteLine("Light is off");
        }
    }

    // Invoker
    class RemoteControl
    {
        private ICommand command;

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void PressButton()
        {
            command.Execute();
        }
    }
    // Command Interface
    interface IFileCommand
    {
        void Execute();
    }

    // Concrete Commands
    class CreateFileCommand : IFileCommand
    {
        private readonly FileReceiver fileReceiver;
        private readonly string fileName;

        public CreateFileCommand(FileReceiver fileReceiver, string fileName)
        {
            this.fileReceiver = fileReceiver;
            this.fileName = fileName;
        }

        public void Execute()
        {
            fileReceiver.CreateFile(fileName);
        }
    }

    class DeleteFileCommand : IFileCommand
    {
        private readonly FileReceiver fileReceiver;
        private readonly string fileName;

        public DeleteFileCommand(FileReceiver fileReceiver, string fileName)
        {
            this.fileReceiver = fileReceiver;
            this.fileName = fileName;
        }

        public void Execute()
        {
            fileReceiver.DeleteFile(fileName);
        }
    }

    // Receiver
    class FileReceiver
    {
        public void CreateFile(string fileName)
        {
            Console.WriteLine($"Created file: {fileName}");
        }

        public void DeleteFile(string fileName)
        {
            Console.WriteLine($"Deleted file: {fileName}");
        }
    }

    // Invoker
    class FileInvoker
    {
        private IFileCommand command;

        public void SetCommand(IFileCommand command)
        {
            this.command = command;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            Light light = new Light();
            RemoteControl remote = new RemoteControl();

            ICommand lightOn = new LightOnCommand(light);
            ICommand lightOff = new LightOffCommand(light);

            remote.SetCommand(lightOn);
            remote.PressButton(); // Turns on the light

            remote.SetCommand(lightOff);
            remote.PressButton(); // Turns off the light

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            FileReceiver fileReceiver = new FileReceiver();
            FileInvoker invoker = new FileInvoker();

            IFileCommand createFileCommand = new CreateFileCommand(fileReceiver, "example.txt");
            IFileCommand deleteFileCommand = new DeleteFileCommand(fileReceiver, "example.txt");

            invoker.SetCommand(createFileCommand);
            invoker.ExecuteCommand(); // Creates a file

            invoker.SetCommand(deleteFileCommand);
            invoker.ExecuteCommand(); // Deletes the file
        }
    }

}

