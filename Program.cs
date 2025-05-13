using System;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class CybersecurityBot
{
    // Automatic properties for personalization and memory
    public string UserName { get; set; }
    public string LastResponse { get; set; }
    public string FavoriteTopic { get; set; } // Memory for favorite topic
    private Dictionary<string, List<string>> keywordResponses;

    // Initialize keyword responses and memory
    public CybersecurityBot()
    {
        keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string>
                {
                    "Use strong, unique passwords for each of your accounts. Avoid using personal details in your passwords.",
                    "Consider using a password manager to keep track of your passwords securely.",
                    "Don't reuse passwords across multiple sites. Each account should have its own unique password."
                }
            },
            { "scam", new List<string>
                {
                    "Be cautious of unsolicited emails or messages asking for personal information.",
                    "If an offer seems too good to be true, it probably is. Always verify the source before clicking any links.",
                    "Watch out for fake social media accounts that mimic real ones to scam users."
                }
            },
            { "privacy", new List<string>
                {
                    "Review the privacy settings on your social media accounts to ensure you're sharing information only with those you trust.",
                    "Use strong two-factor authentication wherever possible to enhance privacy and security.",
                    "Remember that anything you post online could potentially be seen by others—always think before sharing."
                }
            },
            { "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                    "Check the sender's email address carefully. Often, phishing emails come from addresses that look suspicious or misspelled.",
                    "Avoid clicking on links in unsolicited emails. Instead, visit the official website directly to check the validity of the message."
                }
            }
        };
    }

    // Play the voice greeting
    public static void PlayVoiceGreeting()
    {
        try
        {
            using SoundPlayer player = new SoundPlayer("Audio/welcome.wav");
            player.PlaySync();  // Play the sound synchronously
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Error] Unable to play voice greeting: " + ex.Message);
        }
    }

    // Display ASCII Art for the Cybersecurity Awareness Bot logo
    public static void DisplayAsciiLogo()
    {
        string asciiArt = @"
  ____ ____ ____ ____ ____ ____ 
 ||C |||y |||b |||e |||r |||S ||
 ||__|||__|||__|||__|||__|||__|| 
 |/__\|/__\|/__\|/__\|/__\|/__\| 
 Cybersecurity Awareness Bot
        ";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(asciiArt);
        Console.ResetColor();
    }

    // Function to handle user interactions and store their information
    public void GreetUser()
    {
        Console.Clear();
        DisplayAsciiLogo();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Please enter your name: ");
        Console.ResetColor();
        UserName = Console.ReadLine();

        PlayVoiceGreeting();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Hello, {UserName}! Welcome to the Cybersecurity Awareness Bot!");
        Console.ResetColor();
    }

    // Function to recognize and respond to user input
    public void RespondToUser(string userInput)
    {
        string response = string.Empty;

        // Convert input to lowercase to handle case-insensitive responses
        userInput = userInput.ToLower();

        // Keyword Recognition and Random Responses
        foreach (var keyword in keywordResponses.Keys)
        {
            if (userInput.Contains(keyword))
            {
                var responses = keywordResponses[keyword];
                var randomResponse = responses[new Random().Next(responses.Count)];
                response = randomResponse;
                break;
            }
        }

        // Sentiment detection for emotional tone
        if (userInput.Contains("worried") || userInput.Contains("scared"))
        {
            response = "It's completely understandable to feel that way. Cybersecurity can be overwhelming at times, but don't worry, I'm here to help!";
        }
        else if (userInput.Contains("curious"))
        {
            response = "That's great! Being curious about cybersecurity is the first step to staying safe online!";
        }
        else if (string.IsNullOrEmpty(response))
        {
            // Default response when no keywords match
            response = "I didn't quite understand that. Could you rephrase? If you need help with something related to cybersecurity, just ask!";
        }

        LastResponse = response;
        DisplayResponse(response);
    }

    // Function to display responses
    private void DisplayResponse(string response)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nBot Response:");
        Console.ResetColor();
        Console.WriteLine(response);
    }

    // Input validation and interaction loop
    public void StartChatting()
    {
        bool continueChatting = true;

        while (continueChatting)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nYou: ");
            Console.ResetColor();
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please enter something.");
                continue;
            }

            // Respond to user input
            RespondToUser(userInput);

            // Ask if they want to continue the conversation
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Would you like to ask another question? (yes/no): ");
            Console.ResetColor();
            string continueResponse = Console.ReadLine().ToLower();

            if (continueResponse != "yes")
            {
                continueChatting = false;
            }
        }

        // End the conversation
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nThank you for chatting! Stay safe online.");
        Console.ResetColor();
    }

    // Main function to initiate the chatbot
    static void Main(string[] args)
    {
        // Create an instance of the bot
        CybersecurityBot bot = new CybersecurityBot();

        // Greet the user and start the interaction
        bot.GreetUser();
        bot.StartChatting();
    }
}
