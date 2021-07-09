﻿namespace SolutionTemplate.Models

open System

type InfoBarSeverity =
    | Informational = 0
    | Success = 1
    | Warning = 2
    | Error = 3

type Notification =
    { Type : string
      Title : string
      Text : string
      Timeout : TimeSpan voption
      IsOpen : bool
      IsClosable : bool }

module Notification =

    let ErrorWithTimer title text timeout =
        { Type = "Error"
          Title = title
          Text = text
          Timeout = ValueSome timeout
          IsOpen = true
          IsClosable = true }

    let Error title text =
        { Type = "Error"
          Title = title
          Text = text
          Timeout = ValueNone
          IsOpen = true
          IsClosable = true }

    let InfoWithTimer title text timeout =
        { Type = "Informational"
          Title = title
          Text = text
          Timeout = ValueSome timeout
          IsOpen = true
          IsClosable = true }

    let Info title text =
        { Type = "Informational"
          Title = title
          Text = text
          Timeout = ValueNone
          IsOpen = true
          IsClosable = true }
