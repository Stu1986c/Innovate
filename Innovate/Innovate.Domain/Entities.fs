namespace Innovate.Domain

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
}
