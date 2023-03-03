DROP TABLE IF EXISTS "public"."mra_course";
DROP TABLE IF EXISTS "public"."mra_student";
CREATE TABLE "public"."mra_course" ( 
  "id" SERIAL,
  "name" VARCHAR(255) NOT NULL,
  "points" INTEGER NOT NULL,
  "start_date" DATE NOT NULL,
  "end_date" DATE NOT NULL,
  CONSTRAINT "mra_course_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."mra_student" ( 
  "id" SERIAL,
  "first_name" VARCHAR(50) NULL,
  "last_name" VARCHAR(50) NULL,
  "email" VARCHAR(100) NULL,
  "age" INTEGER NULL,
  "password" VARCHAR(50) NULL,
  CONSTRAINT "mra_student_pkey" PRIMARY KEY ("id")
);