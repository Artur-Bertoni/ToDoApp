const API_BASE_URL = "http://localhost:5000/api/tasks/";

export default {
    fetchTasks() {
        fetch(`${API_BASE_URL}`)
            .then((res) => res.json())
            .then((data) => (this.tasks = data));
    },

    addTask() {
        const taskData = {
            name: this.newTask.name,
            description: this.newTask.description,
            completed: this.newTask.completed,
        };

        if (!this.newTask.name || this.newTask.name.trim() === "") {
            alert("O nome da tarefa é obrigatório.");
            return;
        }

        if (!this.newTask.description || this.newTask.description.trim() === "") {
            alert("A descrição da tarefa é obrigatória.");
            return;
        }

        if (this.newTask.deadline) {
            taskData.deadline = this.newTask.deadline;

            const year = this.newTask.deadline.split('-')[0];
            if (year.length > 4) {
                alert("Data inválida: o ano não pode ter mais de 4 dígitos.");
                return;
            }
        }

        if (!this.editingTask) {
            fetch(`${API_BASE_URL}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(taskData)
            })
                .then(response => response.json())
                .then(() => {
                    this.fetchTasks();
                    this.closePopup();
                })
                .catch((error) => console.error('Problema com a operação:', error));
        } else {
            taskData.id = this.editingTask.id;
            fetch(`${API_BASE_URL}${this.editingTask.id}`, {
                method: 'PUT',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(taskData),
            })
                .then(() => {
                    this.fetchTasks();
                    this.closePopup();
                })
                .catch((error) => console.error('Problema com a operação:', error));
        }
    },

    editTask(task) {
        this.newTask = {
            id: task.id,
            name: task.name,
            description: task.description,
            deadline: task.deadline ? task.deadline.split('T')[0] : "",
            completed: task.completed,
        };
        this.editingTask = task;
        this.showPopup = true;
    },

    deleteTask(id) {
        fetch(`${API_BASE_URL}${id}`, {method: 'DELETE'})
            .then(() => this.fetchTasks())
            .catch((error) => console.error('Problema com a operação:', error));
    },

    toggleCompletion(id) {
        const task = this.tasks.find(t => t.id === id);

        fetch(`${API_BASE_URL}${id}/complete`, {method: 'PUT'})
            .then(() => this.fetchTasks())
            .catch((error) => console.error('Problema com a operação:', error));
        if (!task.completed) {
            this.showCompletionPopup();
        }
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

    validateDateLength(event) {
        const date = event.target.value;
        const year = date.split('-')[0];
        if (year.length > 4) {
            event.target.value = '';
            alert("O ano não pode ter mais de 4 dígitos.");
        }
    },

    showCompletionPopup() {
        this.popupVisible = true;
        this.popupProgress = 100;
        this.timeRemaining = 5000;

        this.popupInterval = setInterval(() => {
            if (!this.paused && this.timeRemaining > 0) {
                this.timeRemaining -= 100;
                this.popupProgress = (this.timeRemaining / 5000) * 100;
            }

            if (this.timeRemaining <= 0) {
                this.hideCompletionPopup();
            }
        }, 100);
    },

    hideCompletionPopup() {
        clearInterval(this.popupInterval);
        this.popupVisible = false;
        this.popupProgress = 100;
    },

    pausePopup() {
        this.paused = true;
    },

    resumePopup() {
        this.paused = false;
    },
};
