<template>
  <div id="app">
    <div class="content-wrapper">
      <div class="header">
        <h1>TO DO LIST</h1>
        <button @click="openPopup" class="icon-button add-task-button">
          <img src="../assets/img/add-icon.png" alt="Add Task" class="icon-image add-task-image"/>
        </button>
      </div>

      <ul class="task-list-container">
        <li v-for="task in tasks" :key="task.id" :class="{ 'completed-task': task.completed }">
          <div class="task-container">
            <input type="checkbox" :checked="task.completed" @change="toggleCompletion(task.id)"
                   class="complete-checkbox"/>
            <div class="task-details">
              <div :class="['task-name', { completed: task.completed }]">{{ task.name }}</div>
              <div :class="['task-description', { completed: task.completed }]">{{ task.description }}</div>
              <div
                  :class="{
                    'task-deadline': true,
                    'past-deadline': !task.completed && new Date(task.deadline) < new Date(),
                    'completed': task.completed}">
                Deadline: {{ task.deadline ? new Date(task.deadline).toLocaleDateString('pt-BR') : 'not specified' }}
              </div>


            </div>
          </div>
          <div class="button-group">
            <button @click="editTask(task)" class="icon-button">
              <img src="../assets/img/edit-icon.png" alt="Edit" class="icon-image"/>
            </button>
            <button @click="deleteTask(task.id)" class="icon-button delete-button">
              <img src="../assets/img/delete-icon.png" alt="Delete" class="icon-image"/>
            </button>
          </div>
        </li>
      </ul>

      <div v-if="showPopup" class="popup-overlay" @click.self="closePopup">
        <div class="popup">
          <h2>{{ editingTask ? 'Edit Task' : 'Add Task' }}</h2>
          <form @submit.prevent="addTask">
            <input v-model="newTask.name" placeholder="Task name" required/>
            <input v-model="newTask.description" placeholder="Task description" required/>
            <input type="date" v-model="newTask.deadline" @input="validateDateLength"/>
            <div class="popup-buttons">
              <button type="submit">{{ editingTask ? 'Save Task' : 'Add Task' }}</button>
              <button type="button" @click="closePopup">Cancel</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>


</template>

<script>
export default {
  data() {
    return {
      tasks: [],
      newTask: {name: "", description: "", deadline: "", completed: false},
      editingTask: null,
      showPopup: false
    };
  },
  methods: {
    fetchTasks() {
      fetch("http://localhost:5000/api/tasks")
          .then((res) => res.json())
          .then((data) => (this.tasks = data));
    },
    addTask() {
      const taskData = {
        name: this.newTask.name,
        description: this.newTask.description,
        completed: this.newTask.completed
      };
      if (this.newTask.deadline) {
        taskData.deadline = this.newTask.deadline;
      }
      if (this.newTask.deadline && this.newTask.deadline.split('-')[0].length > 4) {
        alert("Data inválida: o ano não pode ter mais de 4 dígitos.");
        return;
      }
      if (!this.editingTask) {
        fetch(`http://localhost:5000/api/tasks`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(taskData)
        })
            .then(response => response.json())
            .then(data => {
              console.log("Task added successfully");
              this.fetchTasks();
              this.closePopup();
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
          body: JSON.stringify(taskData)
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
      this.closePopup();
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
      this.showPopup = true;
    },
    openPopup() {
      this.resetForm();
      this.showPopup = true;
    },
    closePopup() {
      this.showPopup = false;
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
    },
    validateDateLength(event) {
      const date = event.target.value;
      const year = date.split('-')[0];
      if (year && year.length > 4) {
        event.target.value = '';
        alert("O ano não pode ter mais de 4 dígitos.");
      }
    },
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
