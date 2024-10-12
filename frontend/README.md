# Frontend Documentation

## Overview
The frontend of the "To Do List" application is built using Vue.js. It provides a user interface for creating, editing, and managing tasks with deadlines.

## Technologies Used
- **Framework**: Vue.js
- **Build Tool**: Webpack
- **Package Manager**: npm

## Running the Frontend Locally

### Prerequisites
- **Node.js** (version 14 or above) - [Download](https://nodejs.org/)
- **npm** (comes with Node.js)

### Running the Frontend
1. Navigate to the [`frontend`](./) directory:
   ```bash
   cd frontend
   ```

2. Install the necessary dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run serve
   ```

The frontend will be available at [`http://localhost:8080`](http://localhost:8080).

## Interface Guide

### Task Management Features

- **Add New Task**: Click the ["+" icon](./src/assets/img/add-icon.png) next to the title "TO DO LIST" to open the form for adding a new task.
    - You must provide a **title** and an optional **description** for the task.
    - The **deadline** field is not required, and tasks with expired deadlines will be highlighted in red.

- **Edit Task**: To edit an existing task, click the [pencil icon (✏️)](./src/assets/img/edit-icon.png) next to the task. A pop-up will appear where you can modify the title, description, and deadline (the rules for adding a task are also applied for editing).

- **Complete Task**: To mark a task as complete, simply check the box next to it. Completed tasks will be moved to the bottom of the list, and a popup will appear saying "Task Completed! Congratulations! 🎉" with a progress bar indicating how long the popup will remain visible. The popup countdown will pause if you hover over it.

- **Delete Task**: To delete a task, click the [trash can icon (🗑️)](./src/assets/img/delete-icon.png) next to the task.

### Visual Cues

- **Past Deadline**: If a task’s deadline has passed, the deadline text will turn red.
- **Task Order**: Tasks are displayed in the order of their deadlines. Completed tasks are listed at the bottom, also ordered by their deadline.

### Navigation and Responsiveness
- The layout is responsive and will adjust for different screen sizes.
- Tasks are displayed in a list format, with each task having its own separate box with rounded borders.