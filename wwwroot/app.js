const API_URL = "http://localhost:5072/api/Students";

// Load all students when page loads
document.addEventListener("DOMContentLoaded", () => {
  loadStudents();

  document
    .getElementById("studentForm")
    .addEventListener("submit", handleSubmit);
  document.getElementById("cancelBtn").addEventListener("click", resetForm);
});

// Load all students from API
async function loadStudents() {
  try {
    const response = await fetch(API_URL);
    const students = await response.json();

    displayStudents(students);
  } catch (error) {
    console.error("Error loading students:", error);
    alert("Failed to load students");
  }
}

// Display students in table
function displayStudents(students) {
  const tbody = document.getElementById("studentsTableBody");
  tbody.innerHTML = "";

  if (students.length === 0) {
    tbody.innerHTML =
      '<tr><td colspan="7" style="text-align:center;">No students found</td></tr>';
    return;
  }

  students.forEach((student) => {
    const row = document.createElement("tr");
    const enrollmentDate = new Date(
      student.enrollmentDate,
    ).toLocaleDateString();

    row.innerHTML = `
            <td>${student.id}</td>
            <td>${student.name}</td>
            <td>${student.email}</td>
            <td>${student.age}</td>
            <td>${student.course}</td>
            <td>${enrollmentDate}</td>
            <td class="action-buttons">
                <button class="edit-btn" onclick="editStudent(${student.id})">Edit</button>
                <button class="delete-btn" onclick="deleteStudent(${student.id})">Delete</button>
            </td>
        `;

    tbody.appendChild(row);
  });
}

// Handle form submission (Create or Update)
async function handleSubmit(e) {
  e.preventDefault();

  const studentId = document.getElementById("studentId").value;
  const studentData = {
    name: document.getElementById("name").value,
    email: document.getElementById("email").value,
    age: parseInt(document.getElementById("age").value),
    course: document.getElementById("course").value,
    enrollmentDate: document.getElementById("enrollmentDate").value,
  };

  try {
    let response;

    if (studentId) {
      // Update existing student
      response = await fetch(`${API_URL}/${studentId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(studentData),
      });
    } else {
      // Create new student
      response = await fetch(API_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(studentData),
      });
    }

    if (response.ok) {
      alert(
        studentId
          ? "Student updated successfully!"
          : "Student added successfully!",
      );
      resetForm();
      loadStudents();
    } else {
      alert("Failed to save student");
    }
  } catch (error) {
    console.error("Error saving student:", error);
    alert("Failed to save student");
  }
}

// Edit student
async function editStudent(id) {
  try {
    const response = await fetch(`${API_URL}/${id}`);
    const student = await response.json();

    document.getElementById("studentId").value = student.id;
    document.getElementById("name").value = student.name;
    document.getElementById("email").value = student.email;
    document.getElementById("age").value = student.age;
    document.getElementById("course").value = student.course;
    document.getElementById("enrollmentDate").value =
      student.enrollmentDate.split("T")[0];

    document.getElementById("formTitle").textContent = "Edit Student";
    document.getElementById("submitBtn").textContent = "Update Student";
    document.getElementById("cancelBtn").style.display = "inline-block";

    window.scrollTo({ top: 0, behavior: "smooth" });
  } catch (error) {
    console.error("Error loading student:", error);
    alert("Failed to load student details");
  }
}

// Delete student
async function deleteStudent(id) {
  if (!confirm("Are you sure you want to delete this student?")) {
    return;
  }

  try {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });

    if (response.ok) {
      alert("Student deleted successfully!");
      loadStudents();
    } else {
      alert("Failed to delete student");
    }
  } catch (error) {
    console.error("Error deleting student:", error);
    alert("Failed to delete student");
  }
}

// Reset form
function resetForm() {
  document.getElementById("studentForm").reset();
  document.getElementById("studentId").value = "";
  document.getElementById("formTitle").textContent = "Add New Student";
  document.getElementById("submitBtn").textContent = "Add Student";
  document.getElementById("cancelBtn").style.display = "none";
}
