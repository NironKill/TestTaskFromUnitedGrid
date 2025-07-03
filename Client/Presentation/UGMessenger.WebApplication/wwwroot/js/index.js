document.addEventListener("DOMContentLoaded", function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.start().then(function () {
        let uniqueNumber;
        connection.invoke("GetUniqueNumber", connection.connectionId)
            .then(uniqueNum => {
                uniqueNumber = uniqueNum;
                console.log("Receive ID: ", uniqueNum);
                document.getElementById("UserId").innerText = "UserId to receive message on: " + uniqueNum;
            })
            .catch(err => console.log(err));

        console.log("Connected to SignalR hub. Connection ID: ", connection.connectionId);

        // Receive a message
        connection.on("SignalREvent", function (message, userName) {
            const messagesList = document.getElementById("messages");
            const listItem = document.createElement("li");
            listItem.textContent = `User: ${userName} Message: ${message}`;
            messagesList.appendChild(listItem);
        });

        // Send a message to all users
        document.getElementById("sendToAllButton").addEventListener("click", function () {
            const messageInput = document.getElementById("messageInput");
            const userName = document.getElementById("userName");
            const message = messageInput.value;
            connection.invoke("SendToAll", message, userName.value)
                .catch(err => console.error("Error sending message to all:", err));
            messageInput.value = "";
        });

        // Join a group
        document.getElementById("joinGroupButton").addEventListener("click", function () {
            const groupNameInput = document.getElementById("groupNameInput");
            const userName = document.getElementById("userName");
            alert(userName.value + " Joined the group");
            const groupName = groupNameInput.value;
            connection.invoke("JoinGroup", groupName)
                .catch(err => console.error("Error while joining group:", err));
        });

        document.getElementById("leaveGroupButton").addEventListener("click", function () {
            const groupNameInput = document.getElementById("groupNameInput");
            const userName = document.getElementById("userName");
            alert(userName.value + " Left the group");
            const groupName = groupNameInput.value;
            connection.invoke("LeaveGroup", groupName)
                .catch(err => console.error("Error while leaving group:", err));
        });

        // Send a message to a group
        document.getElementById("sendToGroupButton").addEventListener("click", function () {
            const groupNameInput = document.getElementById("groupNameInput");
            const groupName = groupNameInput.value;
            const messageInput = document.getElementById("messageInput");
            const userName = document.getElementById("userName");
            const message = messageInput.value;
            connection.invoke("SendToGroup", groupName, message, userName.value)
                .catch(err => console.error("Error sending message to group:", err));
            messageInput.value = "";
        });

        // Send a message to a specific user
        document.getElementById("sendToUserButton").addEventListener("click", function () {
            // Get the user ID from the input field
            const userId = document.getElementById("userIdInput").value;
            const userName = document.getElementById("userName").value;
            // Get the message from the input field
            const message = document.getElementById("messageInput").value;

            // Call the SignalR method to send the message to the specified user
            connection.invoke("SendToUser", userId, message, userName)
                .catch(err => console.error("Error sending message to user:", err));

            // Clear the message input field
            document.getElementById("messageInput").value = "";
        });

    }).catch(err => console.error("Error connecting to SignalR hub:", err));
});