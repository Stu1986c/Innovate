namespace Innovate.Db

open FSharp.Data.Sql
open System
open System.Linq
open System.Collections.Generic
open Innovate.Domain

module DataConnection =

    [<Literal>]
    let ConnStr =
        "Data Source=.\\SQLEXPRESS; Initial Catalog= DbInnovate; Integrated Security = true"

    type InnovationConnection = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, ConnStr>

    type DbContext = InnovationConnection.dataContext
    
    let private getContext() = InnovationConnection.GetDataContext()

    type SuggestionEntity = InnovationConnection.dataContext.``dbo.SuggestionsEntity``

    type VoteEntity = InnovationConnection.dataContext.``dbo.VotesEntity``

    let firstOrNone s = s |> Seq.tryFind (fun _ -> true)

    let mapToSuggestion (suggestionEntity : SuggestionEntity) =
        {         
            SuggestionId = suggestionEntity.SuggestionId
            SuggestionText = suggestionEntity.SuggestionText
            Submitter = suggestionEntity.Submitter
            CategoryId = suggestionEntity.CategoryId
            Status = suggestionEntity.Status
        }

    let mapToVote (voteEntity : VoteEntity) = 
        {
            VoteId = voteEntity.VoteId
            SuggestionId = voteEntity.SuggestionId
            Opinion = voteEntity.Opinion
            VoterId = voteEntity.VoterId
        }

    let getAllSuggestions () = 
        let ctx = getContext()
        ctx.Dbo.Suggestions |> Seq.map mapToSuggestion

//    let getAllSuggestionsByCategoryId categoryId = 
//        let ctx = getContext()
//        query {
//            for suggestion in ctx.Dbo.Suggestions do
//                where (suggestion.CategoryId = categoryId)
//                select suggestion
//        } |> Seq.map mapToSuggestion

//    let getAllSuggestionsBySubmitter (ctx : DbContext) submitterName = 
//        query {
//            for suggestion in ctx.Dbo.Suggestions do
//                where (suggestion.Submitter = submitterName)
//                select suggestion
//        } |> Seq.toList


    let getSuggestionById suggestionId = 
        let ctx = getContext()
        query { 
            for suggestion in ctx.Dbo.Suggestions do 
                where (suggestion.SuggestionId = suggestionId) 
                select suggestion 
            } |> Seq.map mapToSuggestion |> Seq.exactlyOne

    let createSuggestion suggestion =
        let ctx = getContext()
        let persistedSuggestion = ctx.Dbo.Suggestions.Create(suggestion.CategoryId, suggestion.Status, suggestion.Submitter, suggestion.SuggestionText)
        ctx.SubmitUpdates()
        persistedSuggestion |> mapToSuggestion

