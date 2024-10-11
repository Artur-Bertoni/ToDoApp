<template>
  <TaskTemplate
      :tasks="tasks"
      :showPopup="showPopup"
      :editingTask="editingTask"
      :newTask="newTask"
      @openPopup="openPopup"
      @closePopup="closePopup"
      @editTask="editTask"
      @deleteTask="deleteTask"
      @toggleCompletion="toggleCompletion"
      @addTask="addTask"
      @validateDateLength="validateDateLength"
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
      newTask: {name: "", description: "", deadline: "", completed: false},
      editingTask: null,
      showPopup: false,
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
