<template>
  <TaskTemplate
      :tasks="tasks"
      :showPopup="showPopup"
      :editingTask="editingTask"
      :newTask="newTask"
      :popupVisible="popupVisible"
      :popupProgress="popupProgress"
      @openPopup="openPopup"
      @closePopup="closePopup"
      @editTask="editTask"
      @deleteTask="deleteTask"
      @toggleCompletion="toggleCompletion"
      @addTask="addTask"
      @validateDateLength="validateDateLength"
      @pausePopup="pausePopup"
      @resumePopup="resumePopup"
  />
</template>

<script>
import TaskTemplate from './TaskTemplate.vue';
import TaskMethods from '../utils/TaskMethods';

export default {
  components: {
    TaskTemplate,
  },
  data() {
    return {
      tasks: [],
      newTask: { name: "", description: "", deadline: "", completed: false },
      editingTask: null,
      showPopup: false,
      popupVisible: false,
      popupProgress: 100,
      popupInterval: null,
      popupTimeout: null,
      paused: false,
      timeRemaining: 5000,
    };
  },
  mounted() {
    this.fetchTasks();
  },
  methods: {
    ...TaskMethods,
  }
};
</script>
