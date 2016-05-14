#I "../packages/Suave/lib/net40"
#I "../packages/FSharp.Data.SqlClient/lib/net40"
#I "../packages/SQLProvider/lib"
#I "../packages/FAKE/tools"
#I "../Innovate.Data"
#I "../Innovate.Domain/bin/debug"
#r "Suave.dll"
#r "FakeLib.dll"
#r "Newtonsoft.Json.dll"
#r "FSharp.Data.SqlClient.dll"
#r "FSharp.Data.SqlProvider.dll"
#r "Innovate.Domain.dll"
#load "Restful.fs"
#load "DataConnection.fs"

open Suave.Web
open Suave.Successful
open Innovate.Suave.Rest
open Innovate.Db
open System
open System.Net
open Fake
open Suave
open Suave.Filters
open Suave.Operators


Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


let suggestionWebPart = rest "suggestions" {
    GetAll = DataConnection.getAllSuggestions
    Create = DataConnection.createSuggestion
    GetById = DataConnection.getSuggestionById
}

//let votingWebPart = rest "votes" {
//}

let port = getBuildParamOrDefault "port" "8083" |> uint16

let config =
    { defaultConfig with
        bindings = [HttpBinding.mk HTTP IPAddress.Loopback port]
    }


startWebServer config (choose[suggestionWebPart])
0 // return an integer exit code
