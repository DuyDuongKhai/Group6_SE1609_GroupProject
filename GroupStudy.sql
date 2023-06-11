CREATE DATABASE GroupStudy;
GO

-- Sử dụng cơ sở dữ liệu
USE GroupStudy;
GO

-- Tạo bảng Users
CREATE TABLE Users (
UserID INT PRIMARY KEY,
Email VARCHAR(255),
Password VARCHAR(255),
Role VARCHAR(50),
FirstName VARCHAR(100),
LastName VARCHAR(100),
DateOfBirth DATE,
PhoneNumber VARCHAR(20),
Address VARCHAR(255)
-- Thêm các cột thông tin khác cho hồ sơ người dùng
-- ...
);
GO

-- Tạo bảng Groups
CREATE TABLE Groups (
GroupID INT PRIMARY KEY,
GroupName VARCHAR(255),
GroupAdminID INT,
CreatedAt DATETIME,
Description VARCHAR(500)
-- Thêm các cột thông tin khác cho nhóm
-- ...
FOREIGN KEY (GroupAdminID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng GroupMembers
CREATE TABLE GroupMembers (
GroupMemberID INT PRIMARY KEY,
GroupID INT,
UserID INT,
Role VARCHAR(50),
Status VARCHAR(50),
JoinedAt DATETIME
-- Thêm các cột thông tin khác cho thành viên nhóm
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Posts
CREATE TABLE Posts (
PostID INT PRIMARY KEY,
GroupID INT,
UserID INT,
Content VARCHAR(1000),
CreatedAt DATETIME
-- Thêm các cột thông tin khác cho bài đăng
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Comments
CREATE TABLE Comments (
CommentID INT PRIMARY KEY,
PostID INT,
UserID INT,
Content VARCHAR(1000),
CreatedAt DATETIME
-- Thêm các cột thông tin khác cho bình luận
-- ...
FOREIGN KEY (PostID) REFERENCES Posts(PostID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Tasks
CREATE TABLE Tasks (
TaskID INT PRIMARY KEY,
GroupID INT,
AssignedToUserID INT,
TaskTitle VARCHAR(255),
Description VARCHAR(500),
Status VARCHAR(50)
-- Thêm các cột thông tin khác cho nhiệm vụ
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
FOREIGN KEY (AssignedToUserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Meetings
CREATE TABLE Meetings (
MeetingID INT PRIMARY KEY,
GroupID INT,
MeetingTitle VARCHAR(255),
MeetingDateTime DATETIME,
Location VARCHAR(255),
Description VARCHAR(500)
-- Thêm các cột thông tin khác cho cuộc họp
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID)
);
GO
-- Tạo bảng JoinRequests
CREATE TABLE JoinRequests (
RequestID INT PRIMARY KEY,
GroupID INT,
UserID INT,
Status VARCHAR(50)
-- Thêm các cột thông tin khác cho yêu cầu tham gia
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng ChatMessages
CREATE TABLE ChatMessages (
MessageID INT PRIMARY KEY,
GroupID INT,
UserID INT,
Content VARCHAR(1000),
SentAt DATETIME
-- Thêm các cột thông tin khác cho tin nhắn chat
-- ...
FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Thêm dữ liệu vào bảng Users
INSERT INTO Users (UserID, Email, Password, Role, FirstName, LastName, DateOfBirth, PhoneNumber, Address)
VALUES
(1, 'user1@example.com', 'password1', 'User', 'John', 'Doe', '1990-01-01', '123456789', '123 Street'),
(2, 'user2@example.com', 'password2', 'User', 'Jane', 'Smith', '1995-05-10', '987654321', '456 Avenue');
-- Thêm dữ liệu vào bảng Groups
INSERT INTO Groups (GroupID, GroupName, GroupAdminID, CreatedAt, Description)
VALUES
(1, 'Group A', 1, GETDATE(), 'This is Group A'),
(2, 'Group B', 2, GETDATE(), 'This is Group B');
-- Thêm dữ liệu vào bảng GroupMembers
INSERT INTO GroupMembers (GroupMemberID, GroupID, UserID, Role, Status, JoinedAt)
VALUES
(1, 1, 1, 'Member', 'Active', GETDATE()),
(2, 1, 2, 'Member', 'Active', GETDATE()),
(3, 2, 1, 'Member', 'Active', GETDATE());
-- Thêm dữ liệu vào bảng Posts
INSERT INTO Posts (PostID, GroupID, UserID, Content, CreatedAt)
VALUES
(1, 1, 1, 'Hello Group A!', GETDATE()),
(2, 1, 2, 'Hi everyone!', GETDATE()),
(3, 2, 1, 'Welcome to Group B!', GETDATE());
-- Thêm dữ liệu vào bảng Comments
INSERT INTO Comments (CommentID, PostID, UserID, Content, CreatedAt)
VALUES
(1, 1, 2, 'Nice post!', GETDATE()),
(2, 2, 1, 'Thanks!', GETDATE());
-- Thêm dữ liệu vào bảng Tasks
INSERT INTO Tasks (TaskID, GroupID, AssignedToUserID, TaskTitle, Description, Status)
VALUES
(1, 1, 1, 'Task 1', 'Description for Task 1', 'Incomplete'),
(2, 2, 2, 'Task 2', 'Description for Task 2', 'Complete');
-- Thêm dữ liệu vào bảng Meetings
INSERT INTO Meetings (MeetingID, GroupID, MeetingTitle, MeetingDateTime, Location, Description)
VALUES
(1, 1, 'Meeting 1', '2023-06-05 10:00:00', 'Meeting Room A', 'Discussion for Group A'),
(2, 2, 'Meeting 2', '2023-06-10 15:00:00', 'Meeting Room B', 'Discussion for Group B');
-- Thêm dữ liệu vào bảng JoinRequests
INSERT INTO JoinRequests (RequestID, GroupID, UserID, Status)
VALUES
(1, 2, 2, 'Pending'),
(2, 1, 2, 'Approved');
-- Thêm dữ liệu vào bảng ChatMessages (tiếp tục)
INSERT INTO ChatMessages (MessageID, GroupID, UserID, Content, SentAt)
VALUES
(1, 1, 1, 'Hello everyone!', GETDATE()),
(2, 2, 2, 'Welcome to Group B!', GETDATE());

-- Tạo dữ liệu vào bảng Comments (tiếp tục)
INSERT INTO Comments (CommentID, PostID, UserID, Content, CreatedAt)
VALUES
(3, 1, 1, 'Thanks!', GETDATE()),
(4, 2, 2, 'I agree!', GETDATE());

-- Thêm dữ liệu vào bảng Tasks (tiếp tục)
INSERT INTO Tasks (TaskID, GroupID, AssignedToUserID, TaskTitle, Description, Status)
VALUES
(3, 1, 2, 'Task 3', 'Description for Task 3', 'Incomplete'),
(4, 2, 1, 'Task 4', 'Description for Task 4', 'Incomplete');

-- Thêm dữ liệu vào bảng Meetings (tiếp tục)
INSERT INTO Meetings (MeetingID, GroupID, MeetingTitle, MeetingDateTime, Location, Description)
VALUES
(3, 1, 'Meeting 3', '2023-06-15 14:00:00', 'Meeting Room C', 'Discussion for Group A'),
(4, 2, 'Meeting 4', '2023-06-20 16:00:00', 'Meeting Room D', 'Discussion for Group B');

-- Thêm dữ liệu vào bảng JoinRequests (tiếp tục)
INSERT INTO JoinRequests (RequestID, GroupID, UserID, Status)
VALUES
(3, 1, 2, 'Pending'),
(4, 2, 1, 'Approved');

-- Thêm dữ liệu vào bảng ChatMessages (tiếp tục)
INSERT INTO ChatMessages (MessageID, GroupID, UserID, Content, SentAt)
VALUES
(3, 1, 2, 'I have a question.', GETDATE()),
(4, 2, 1, 'Let''s schedule a meeting.', GETDATE());