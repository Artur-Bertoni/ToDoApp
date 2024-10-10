<template>
  <div>
    <h1>To Do List</h1>
    <ul>
      <li v-for="task in tasks" :key="task.id">
        <span :class="{ completed: task.completed }">
          {{ task.name }} - {{ task.description }} (Prazo: {{ new Date(task.deadline).toLocaleDateString() }})
        </span>
        <button @click="markAsComplete(task.id)">Complete</button>
        <button @click="editTask(task)">Edit</button>
        <button @click="deleteTask(task.id)">Delete</button>
      </li>
    </ul>
    <br>
    <form @submit.prevent="addTask">
      <input v-model="newTask.name" placeholder="Task name" required/>
      <input v-model="newTask.description" placeholder="Task description"/>
      <input type="date" v-model="newTask.deadline" placeholder="Deadline" required/>
      <button type="submit">{{ editingTask ? 'Edit Task' : 'Add Task' }}</button>
    </form>
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
      const url = this.editingTask ? `http://localhost:5000/api/tasks/${this.editingTask.id}` : "http://localhost:5000/api/tasks";
      const method = this.editingTask ? "PUT" : "POST";

      fetch(url, {
        method: method,
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.newTask)
      })
          .then((response) => {
            if (!response.ok) {
              throw new Error('Network response was not ok.');
            }
            return response.json();
          })
          .then((data) => {
            if (this.editingTask) {
              const index = this.tasks.findIndex(task => task.id === data.id);
              this.tasks.splice(index, 1, data);
              this.editingTask = null;
            } else {
              this.tasks.push(data);
            }
            this.newTask = {name: "", description: "", deadline: "", completed: false};
          })
          .catch((error) => console.error('There was a problem with the fetch operation:', error));
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
    markAsComplete(id) {
      fetch(`http://localhost:5000/api/tasks/${id}/complete`, {method: "PUT"})
          .then(response => {
            if (!response.ok) {
              throw new Error("Network response was not ok");
            }
            return this.fetchTasks();
          })
          .catch(error => console.error("There was a problem with the fetch operation:", error));
    },
  },
  mounted() {
    this.fetchTasks();
  },
};
</script>

<style scoped>
.completed {
  text-decoration: line-through;
}
</style>
    