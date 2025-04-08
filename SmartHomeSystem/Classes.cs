namespace SmartHomeSystem;

// All the code in this file is included in all platforms.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Graphics;



    public class Room
    {
        public string Name { get; set; }
        public List<Device> Devices { get; set; } = new List<Device>();
        public List<ScheduledAction> Schedule { get; set; } = new List<ScheduledAction>();

        public Room(string name)
        {
            Name = name;
        }

        public void AddDevice(Device device)
        {
            if (!Devices.Any(d => d.Name == device.Name))
                Devices.Add(device);
        }

        public void RemoveDevice(string deviceName)
        {
            var device = Devices.FirstOrDefault(d => d.Name == deviceName);
            if (device != null)
                Devices.Remove(device);
        }

    public Device? GetDevice(string name) =>
    Devices.FirstOrDefault(d => d.Name == name);

    public void ScheduleDeviceAction(string deviceName, DateTime time, bool turnOn)
        {
            var device = GetDevice(deviceName);
            if (device != null)
            {
                Schedule.Add(new ScheduledAction
                {
                    DeviceName = deviceName,
                    TimeBegin = time,
                    TurnOn = turnOn
                });
            }
        }

        public void RunScheduledActions(DateTime currentTime)
        {
            var actionsToRun = Schedule.Where(s => s.TimeBegin <= currentTime).ToList();
            foreach (var action in actionsToRun)
            {
                var device = GetDevice(action.DeviceName);
                if (device != null)
                    device.IsOn = action.TurnOn;

                Schedule.Remove(action);
            }
        }
    }

    public class Device
    {
        public required string Name { get; set; }
        public bool IsOn { get; set; }

        public virtual void Toggle() => IsOn = !IsOn;
    }

    public class ScheduledAction
    {
        public required string DeviceName { get; set; }
        public DateTime TimeBegin { get; set; }

        public DateTime TimeEnd { get; set; }

        public bool TurnOn { get; set; }
    }

  public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool ValidatePassword(string input)
        {
            return Password == input;
        }
    }


      public class Thermostat : Device
    {
        public double Temperature { get; set; } = 22.0; // default to 22°C

        public void SetTemperature(double temp)
        {
            Temperature = temp;
            IsOn = true;
        }
    }


     public class Light : Device
    {
        public int Brightness { get; set; } = 100;
        public Color Color { get; set; } = Colors.White;

        public void SetBrightness(int brightness)
        {
            Brightness = Math.Clamp(brightness, 0, 100);
        }

        public void SetColor(Color color)
        {
            Color = color;
            IsOn = true;
        }
    }


    public class DoorLock : Device
    {
        public bool IsLocked { get; private set; } = true;

        public void Lock()
        {
            IsLocked = true;
            IsOn = false;
        }

        public void Unlock()
        {
            IsLocked = false;
            IsOn = true;
        }

        public override void Toggle()
        {
            if (IsLocked)
                Unlock();
            else
                Lock();
        }
    }

    public class MotionDetector : Device
    {
        public bool MotionDetected { get; private set; }

        // Event for when motion is detected
        public event EventHandler<string>? OnMotionDetected;

        public void DetectMotion()
        {
            MotionDetected = true;
            IsOn = true;

            // Trigger simulated notification
            OnMotionDetected?.Invoke(this, $"{Name}: Motion detected!");
        }

        public void ClearMotion()
        {
            MotionDetected = false;
            IsOn = false;
        }
    }



     public class Camera : Device
    {
        public bool IsRecording { get; private set; } = false;

        // Simulate recording functionality
        public void StartRecording()
        {
            if (!IsRecording)
            {
                IsRecording = true;
                Console.WriteLine($"{Name} started recording.");
            }
            else
            {
                Console.WriteLine($"{Name} is already recording.");
            }
        }

        public void StopRecording()
        {
            if (IsRecording)
            {
                IsRecording = false;
                Console.WriteLine($"{Name} stopped recording.");
            }
            else
            {
                Console.WriteLine($"{Name} was not recording.");
            }
        }

        // Simulate displaying the camera feed
        public void DisplayFeed()
        {
            if (IsOn)
            {
                Console.WriteLine($"{Name} is displaying the feed.");
            }
            else
            {
                Console.WriteLine($"{Name} is off. Turn it on to display feed.");
            }
        }

        // Override the Toggle method to turn the camera on or off
        public override void Toggle()
        {
            base.Toggle();
            Console.WriteLine(IsOn ? $"{Name} is now on." : $"{Name} is now off.");
        }
    }
