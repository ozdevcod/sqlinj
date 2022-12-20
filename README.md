## sqlinj

https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca3001

#### **Defense Options .net sql server**
> https://learn.microsoft.com/en-us/sql/relational-databases/security/sql-injection?view=sql-server-ver16
___

#### **Samples**

> url<br/>
> https://webserver/Books/Edit?id=45%27;create%20table%20new_table(id%20int);--
> <br/>sqlSentenceExecuted<br/>
> select * from books where id = '45';create table new_table(id int);--'


> url<br/>
> https://webserver/Books/Edit?id=45%27;DROP%20TABLE%20NEW_TABLE;--%27%27
> <br/>sqlSentenceExecuted<br/>
> select * from books where id = '45';DROP TABLE NEW_TABLE;--'''


> url<br/>
> https://webserver/Books/Edit?id=1598919%27or%201=1;--%27
> <br/>sqlSentenceExecuted<br/>
> select * from books where id = '1598919'or 1=1;--''
___

### **OWASP main article for prevention**
> https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html

#### **Defense Option 1: Prepared Statements (with Parameterized Queries)**
> https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html#defense-option-1-prepared-statements-with-parameterized-queries

#### **Defense Option 2: Stored Procedures**
> https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html#defense-option-2-stored-procedures

#### **Defense Option 3: Allow-list Input Validation**
> https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html#defense-option-3-allow-list-input-validation

#### **Defense Option 4: Escaping All User-Supplied Input**
> https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html#defense-option-4-escaping-all-user-supplied-input
