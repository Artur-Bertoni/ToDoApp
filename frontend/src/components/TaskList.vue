<template>
  <div id="app">
    <div class="content-wrapper">
      <h1>To Do List</h1>
      <ul class="task-list-container">
        <li v-for="task in tasks" :key="task.id" :class="{ 'completed-task': task.completed }">
          <div class="task-container">
            <div :class="['task-name', { completed: task.completed }]">{{ task.name }}</div>
            <div :class="['task-description', { completed: task.completed }]">{{ task.description }}</div>
            <div class="task-deadline">Deadline: {{ new Date(task.deadline).toLocaleDateString('pt-BR') }}</div>
          </div>
          <div class="button-group">
            <button @click="toggleCompletion(task.id)">{{ task.completed ? 'Uncomplete' : 'Complete' }}</button>
            <button @click="editTask(task)">Edit</button>
            <button @click="deleteTask(task.id)" class="delete-button">Delete</button>
          </div>
        </li>
      </ul>
      <form @submit.prevent="addTask">
        <input v-model="newTask.name" placeholder="Task name" required />
        <input v-model="newTask.description" placeholder="Task description" />
        <input type="date" v-model="newTask.deadline" placeholder="Deadline" required />
        <button type="submit">{{ editingTask ? 'Edit Task' : 'Add Task' }}</button>
        <button v-if="editingTask" type="button" @click="cancelEdit">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      tasks: [],
      newTask: {name: "", description: "", deadline: "", completed: false},
      editingTask: null
    };
  },
  methods: {
    fetchTasks() {
      fetch("http://localhost:5000/api/tasks")
          .then((res) => res.json())
          .then((data) => (this.tasks = data));
    },
    addTask() {
      if (!this.editingTask) {
        fetch(`http://localhost:5000/api/tasks`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.newTask)
        })
            .then(response => response.json())
            .then(data => {
              console.log("Task added successfully");
              this.fetchTasks();
              this.resetForm();
            })
            .catch(error => {
              console.error('There was a problem with the fetch operation:', error);
            });
      } else {
        fetch(`http://localhost:5000/api/tasks/${this.editingTask.id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.newTask)
        })
            .then(response => {
              if (response.status === 204) {
                console.log("Task updated successfully");
                this.fetchTasks();
              } else {
                return response.json();
              }
            })
            .catch(error => {
              console.error('There was a problem with the fetch operation:', error);
            });
      }

      this.resetForm();
    },
    editTask(task) {
      this.newTask = {
        id: task.id,
        name: task.name,
        description: task.description,
        deadline: task.deadline.split('T')[0],
        completed: task.completed
      };
      this.editingTask = task;
    },
    cancelEdit() {
      this.resetForm();
    },
    resetForm() {
      this.newTask = {name: "", description: "", deadline: "", completed: false};
      this.editingTask = null;
    },
    deleteTask(id) {
      fetch(`http://localhost:5000/api/tasks/${id}`, {method: "DELETE"})
          .then(response => {
            if (!response.ok) {
              throw new Error("Network response was not ok");
            }
            return this.fetchTasks();
          })
          .catch(error => console.error("There was a problem with the fetch operation:", error));
    },
    toggleCompletion(id) {
      fetch(`http://localhost:5000/api/tasks/${id}/complete`, {method: "PUT"})
          .then(response => {
            if (response.status === 204) {
              console.log("Task completion status toggled.");
              this.fetchTasks();
            } else {
              throw new Error("Failed to toggle task completion.");
            }
          })
          .catch(error => console.error("There was a problem with the fetch operation:", error));
    }
  },
  mounted() {
    this.fetchTasks();
  }
};
</script>

<style scoped>
.completed {
  text-decoration: line-through;
}
</style>
