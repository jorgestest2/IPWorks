#
# IPWorks 2022 Python Edition - Demo Application
#
# Copyright (c) 2023 /n software inc. - All rights reserved. - www.nsoftware.com
#

import sys
import string
from ipworks import *

input = sys.hexversion<0x03000000 and raw_input or input

netcode = NetCode()

try:
  print("This sample console application can encode or decode files in a variety of formats. Type '?' for a list of commands.")
  
  while True:
    command = input("> ")

    if len(command) > 0:
      arguments = command.split()
      command = arguments.pop(0).strip().lower()

    if command == "?" or command == "help":
      print("Commands:")
      print("  ?                                           display the list of valid commands")
      print("  help                                        display the list of valid commands")
      print("  encode <input file> <output file> <format>  encode a file")
      print("  decode <input file> <output file> <format>  decode a file")
      print("  quit                                        exit/quit")
      print("Formats:")
      print("  0                                           fmtUUEncode")
      print("  1                                           fmtBase64")
      print("  2                                           fmtQP")
      print("  3                                           fmtURL")
      print("  4                                           fmtJIS")
      print("  5                                           fmtYEncode")
      print("  6                                           fmtMD5Hash")
      print("  7                                           fmtSHA1Hash")
      print("  8                                           fmtHex")
      print("  9                                           fmtHTML")
      print("  10                                          fmtHMAC")
      print("  11                                          fmtUTF8")
      print("  12                                          fmtUTF7")
      print("  13                                          fmtBase32")
      print("  14                                          fmtBase64URL")
      print("  15                                          fmtSHA256")
      print("  16                                          fmtPunycode")

    elif command == "encode":
      netcode.reset()
      netcode.set_overwrite(True)
      netcode.set_decoded_file(arguments[0])
      netcode.set_encoded_file(arguments[1])
      netcode.set_format(int(arguments[2]))
      netcode.encode()

    elif command == "decode":
      netcode.reset()
      netcode.set_overwrite(True)
      netcode.set_encoded_file(arguments[0])
      netcode.set_decoded_file(arguments[1])
      netcode.set_format(int(arguments[2]))
      netcode.decode()

    elif command == "quit":
      print("Goodbye.")
      break

    else:
        print("Invalid command.")

except IPWorksNetcodeError as e:
  print("ERROR [JSON]: %s" % e.message)

