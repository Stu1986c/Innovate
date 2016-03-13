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
    StakeholderId : int
}

type Category = {
    CategoryId : int
    CategoryText : string
    CategoryOwner : string
}

type Suggestion = {
    SuggestionId : int
    SuggestionText : string
    Submitter : string
    Category : Category
    Status : string
    Votes : Vote list
}

