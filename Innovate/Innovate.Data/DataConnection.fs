module Innovate.Db

open FSharp.Data.Sql
open System
open System.Linq

[<Literal>]
let ConnStr =
    "Data Source=.\\SQLEXPRESS; Initial Catalog= DbInnovate; Integrated Security = true"

type InnovationConnection = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnStr>

type DbContext = InnovationConnection.dataContext

type Suggestion = InnovationConnection.dataContext.``dbo.SuggestionsEntity``

let firstOrNone s = s |> Seq.tryFind (fun _ -> true)

let getAllSuggestions (ctx : DbContext) = ctx.Dbo.Suggestions |> Seq.toList

let getAllSuggestionsByCategoryId (ctx : DbContext) categoryId = 
    query {
        for suggestion in ctx.Dbo.Suggestions do
            where (suggestion.CategoryId = categoryId)
            select suggestion
    } |> Seq.toList

let getAllSuggestionsBySubmitter (ctx : DbContext) submitterName = 
    query {
        for suggestion in ctx.Dbo.Suggestions do
            where (suggestion.Submitter = submitterName)
            select suggestion
    } |> Seq.toList

let getAllSuggestionsBySuggestionId suggestionId (ctx : DbContext) : Suggestion option = 
    query {
        for suggestion in ctx.Dbo.Suggestions do
            where (suggestion.SuggestionId = suggestionId)
            select suggestion
    } |> firstOrNone

