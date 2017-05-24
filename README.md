# Automatic-Course-Test-System
## 界面结构：

>Sign_in 登陆
>>User 用户
>>>Test 测试选择
>>>>Specific_Test 具体测试
>>>>>Results 结果

>>>Inquiry 查询
>>>>Results 结果

>>Administrator 管理员
>>>Examinee 考生管理
>>>>InquiryByExaminee 按考生查询
>>>>>ExamineeInformation 考生信息

>>>>InauiryByTest 按课程查询
>>>>>ExamineeInformation 考生信息

>>>QuestionBank 题库管理
>>>>QuestionBankInformation 题库查询、更改

>>>>CreateQuestionBank 新建题库
>>>>>QuestionBankInformation 题库内容增加、更改

>>Registered 注册

## 类
>Class_Examinee 考生类
>>examineenumber 考生编号
>>name 考生姓名
>>classroom 考生班级

>Class_Test 题目类
>>subject 科目
>>test 测试
>>testnumber 编号
>>question 题目
>>type 类型
>>choice_answerA 选择题答案A
>>choice_answerB 选择题答案B
>>choice_answerC 选择题答案C
>>choice_answerD 选择题答案D

>Class_Result 成绩类
>>examineenumber 考生编号
>>subject 科目
>>test 测试
>>score 成绩

>Class_Upload 上传答案类
>>examineenumber 考生编号
>>subject 科目
>>test 测试
>>testnumber 编号
>>choice_answer 选择题答案
>>answer 简答题答案
