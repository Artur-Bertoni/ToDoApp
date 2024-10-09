
<template>
	<div>
		<h1>To Do List</h1>
		<ul>
			<li v-for="task in tasks" :key="task.id">
				<span :class="{ completed: task.completed }">{{ task.name }} - {{ task.description }}</span>
				<button @click="markAsComplete(task.id)">Complete</button>
				<button @click="editTask(task)">Edit</button>
				<button @click="deleteTask(task.id)">Delete</button>
			</li>
		</ul>
		<form @submit.prevent="addTask">
			<input v-model="newTask.name" placeholder="Task name" required />
			<input v-model="newTask.description" placeholder="Task description" />
			<button type="submit">Add Task</button>
		</form>
	</div>
</template>

<script>
export default {
	data() {
		return {
		  tasks: [],
		  newTask: { name: "", description: "" },
		};
	},
	methods: {
		fetchTasks() {
			fetch("http://localhost:5001/api/tasks")
				.then((res) => res.json())
				.then((data) => (this.tasks = data));
		},
		addTask() {
			fetch("http://localhost:5001/api/tasks", {
				method: "POST",
				headers: { "Content-Type": "application/json" },
				body: JSON.stringify(this.newTask),
			})
			.then(response => {
				if (!response.ok) {
					throw new Error("Network response was not ok");
				}
				return this.fetchTasks();
			})
			.catch(error => console.error("There was a problem with the fetch operation:", error));
		},
		editTask(task) {
			// Implementation for task edit
		},
		deleteTask(id) {
			fetch(`http://localhost:5001/api/tasks/${id}`, { method: "DELETE" })
				.then(response => {
					if (!response.ok) {
						throw new Error("Network response was not ok");
					}
					return this.fetchTasks();
				})
				.catch(error => console.error("There was a problem with the fetch operation:", error));
		},
		markAsComplete(id) {
			fetch(`http://localhost:5001/api/tasks/${id}/complete`, { method: "PUT" })
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
    