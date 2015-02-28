int totalCount = 151;
int currentPage = 0;
decimal perPageCount = 50;
int i = 0;
while (i <= totalCount && totalCount != 0)
{
if (i % perPageCount == 0) // increase page by 1, this could have been handled in many ways
    {
        currentPage++;

        url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + currentPage; 

        JsonString = getResponse(url);

        JsonObject = Decode(JsonString);
        

        current_event = JsonOject.event[i%50]; <-- Here is the place we have no common.


        if (current_event.status == "alive")
            List.add(current_event)


        i++;
    }
return List
}
