<template>
  <div id="app">
    <div class="content-wrapper">
      <div class="header">
        <h1>TO DO LIST</h1>
        <button @click="$emit('openPopup')" class="icon-button add-task-button">
          <img src="../assets/img/add-icon.png" alt="Add Task" class="icon-image add-task-image"/>
        </button>
      </div>

      <ul class="task-list-container">
        <li v-for="task in tasks" :key="task.id" :class="{ 'completed-task': task.completed }">
          <div class="task-container">
            <input type="checkbox" :checked="task.completed" @change="$emit('toggleCompletion', task.id)"
                   class="complete-checkbox"/>
            <div class="task-details">
              <div :class="['task-name', { completed: task.completed }]">{{ task.name }}</div>
              <div :class="['task-description', { completed: task.completed }]">{{ task.description }}</div>
              <div
                  :class="['task-deadline', { 'past-deadline': !task.completed && new Date(task.deadline) < new Date() && task.deadline, 'completed': task.completed }]">
                Deadline: {{ task.deadline ? new Date(task.deadline).toLocaleDateString('pt-BR') : 'not specified' }}
              </div>
            </div>
          </div>
          <div class="button-group">
            <button @click="$emit('editTask', task)" class="icon-button">
              <img src="../assets/img/edit-icon.png" alt="Edit" class="icon-image"/>
            </button>
            <button @click="$emit('deleteTask', task.id)" class="icon-button delete-button">
              <img src="../assets/img/delete-icon.png" alt="Delete" class="icon-image"/>
            </button>
          </div>
        </li>
      </ul>

      <div v-if="showPopup" class="popup-overlay" @click.self="$emit('closePopup')">
        <div class="popup">
          <h2>{{ editingTask ? 'Edit Task' : 'Add Task' }}</h2>
          <form @submit.prevent="$emit('addTask')">
            <input v-model="newTask.name" placeholder="Task name" required/>
            <input v-model="newTask.description" placeholder="Task description" required/>
            <input type="date" v-model="newTask.deadline" @input="$emit('validateDateLength', $event)"/>
            <div class="popup-buttons">
              <button type="submit">{{ editingTask ? 'Save Task' : 'Add Task' }}</button>
              <button type="button" @click="$emit('closePopup')">Cancel</button>
            </div>
          </form>
        </div>
      </div>

      <div v-if="popupVisible" class="completion-popup" @mouseenter="$emit('pausePopup')"
           @mouseleave="$emit('resumePopup')">
        <span>Task Completed! Congratulations! 🎉</span>
        <div class="progress-bar" :style="{ width: popupProgress + '%' }"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['tasks', 'showPopup', 'editingTask', 'newTask', 'popupVisible', 'popupProgress'],
};
</script>
