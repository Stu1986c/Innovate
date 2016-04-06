namespace Innovate.Domain

type Stakeholder = {
    StakeholderId : int
    EmailAddress : string
    Forename : string
    Surname : string
}

type Vote = {
    VoteId : int
    Opinion : int
    VoterId : int
    SuggestionId : int
}

//type Category = {
//    CategoryId : int
//    CategoryText : string
//    CategoryOwner : string
//}

type Suggestion = {
    SuggestionId : int
    SuggestionText : string
    Submitter : string
    CategoryId : int
    Status : string
    //Votes : Vote list
}

