/*
 * IPWorks 2022 .NET Edition - Sample Project
 *
 * This sample project demonstrates the usage of one or more classes in a 
 * simple, straightforward way.  This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */

using System.Collections.Generic;
﻿using System;
using System.Threading.Tasks;
using nsoftware.async.IPWorks;

class htmlmailerDemo
{
  private static Htmlmailer htmlmailer;

  private static void htmlmailer_OnSSLServerAuthentication(object sender, HtmlmailerSSLServerAuthenticationEventArgs e)
  {
    if (e.Accept) return;
    Console.Write("Server provided the following certificate:\nIssuer: " + e.CertIssuer + "\nSubject: " + e.CertSubject + "\n");
    Console.Write("The following problems have been determined for this certificate: " + e.Status + "\n");
    Console.Write("Would you like to continue anyways? [y/n] ");
    if (Console.Read() == 'y') e.Accept = true;
  }

  static async Task Main(string[] args)
  {

    htmlmailer = new Htmlmailer();

    if (args.Length < 3)
    {

      Console.WriteLine("usage: htmlmailer [options] server from to");
      Console.WriteLine("Options: ");
      Console.WriteLine("  -s      the subject of the mail message");
      Console.WriteLine("  -m      the HTML version of the message content");
      Console.WriteLine("  -a      the path of file to attach to the message");
      Console.WriteLine("  server  the name or address of a mail server (mail relay)");
      Console.WriteLine("  from    the email address of the sender");
      Console.WriteLine("  to      a comma separated list of addresses for destinations");
      Console.WriteLine("\r\nExample: htmlmailer -s test -m \"<b>Hello</b>, my name is <i>Tom</i>\" -a FileToAttach mail.local sender@mail.com recipient@mail.local");

    }
    else
    {
      try {
        htmlmailer.OnSSLServerAuthentication += htmlmailer_OnSSLServerAuthentication;

        htmlmailer.MailServer = args[args.Length - 3];
        htmlmailer.From = args[args.Length - 2];
        htmlmailer.SendTo = args[args.Length - 1];

        for (int i = 0; i < args.Length; i++)
        {
          if (args[i].StartsWith("-"))
          {
            if (args[i].Equals("-s")) htmlmailer.Subject = args[i + 1]; // args[i+1] corresponds to the value of argument [i]
            if (args[i].Equals("-m")) htmlmailer.MessageHTML = args[i + 1];
            if (args[i].Equals("-a")) await htmlmailer.AddAttachment(args[i + 1]); //if you want to add attachment
          }
        }

        //Use these properties for client authentication
        //htmlmailer.setUser(prompt("User"));
        //htmlmailer.setPassword(prompt("Password"));

        Console.WriteLine("Sending message ...");
        await htmlmailer.Send();

        Console.WriteLine("Message sent successfully");
      } catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}



class ConsoleDemo
{
  public static Dictionary<string, string> ParseArgs(string[] args)
  {
    Dictionary<string, string> dict = new Dictionary<string, string>();

    for (int i = 0; i < args.Length; i++)
    {
      // If it starts with a "/" check the next argument.
      // If the next argument does NOT start with a "/" then this is paired, and the next argument is the value.
      // Otherwise, the next argument starts with a "/" and the current argument is a switch.

      // If it doesn't start with a "/" then it's not paired and we assume it's a standalone argument.

      if (args[i].StartsWith("/"))
      {
        // Either a paired argument or a switch.
        if (i + 1 < args.Length & !args[i + 1].StartsWith("/"))
        {
          // Paired argument.
          dict.Add(args[i].TrimStart('/'), args[i + 1]);
          // Skip the value in the next iteration.
          i++;
        }
        else
        {
          // Switch, no value.
          dict.Add(args[i].TrimStart('/'), "");
        }
      }
      else
      {
        // Standalone argument. The argument is the value, use the index as a key.
        dict.Add(i.ToString(), args[i]);
      }
    }
    return dict;
  }
}